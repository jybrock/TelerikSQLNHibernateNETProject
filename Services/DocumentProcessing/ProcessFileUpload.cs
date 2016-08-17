using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using DomainObjects;
using BusinessServices;
using System.Configuration;
using FREngine;
using AbbyTest;

namespace DocumentProcessing
{
    public class ProcessFileUpload
    {
        PollingFileWatcher _FileWatcher = new PollingFileWatcher(15000); // watch every 30 secs
        static EngineLoader.EngineLoader engineLoader = null;

        public ProcessFileUpload()
        {
            
        }

        public void StartFileWatcher()
        {
            _FileWatcher.AddDirectory(ConfigurationManager.AppSettings["DocumentDirectory"]);
            EventLog.WriteEntry("Document Processing File Uploads", DateTime.Now.ToLongTimeString() + " - File Watcher created for " + ConfigurationManager.AppSettings["DocumentDirectory"]);

            _FileWatcher.CreateFileCallback = new PollingFileWatcher.CreateFile(FileUploaded);
            _FileWatcher.StartWatching();
        }

        private void FileUploaded(FileInfo fileInfo)
        {
            try
            {
                DateTime dt = DateTime.Now;

                bool fileAvailable = false;

                TimeSpan timeOut = new TimeSpan(0, 30, 0);

                while (DateTime.Now - dt < timeOut)
                {
                    try
                    {
                        using (FileStream fs =
                            new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            fileAvailable = true;
                            break;
                        }
                    }
                    catch (IOException)
                    { Thread.Sleep(100); }
                }

                if (!fileAvailable)
                {
                    EventLog.WriteEntry("Document Processing File Uploads", DateTime.Now.ToLongTimeString() +
                                            " - File upload timed out: " + fileInfo.FullName);
                }
                else
                {
                    EventLog.WriteEntry("Document Processing File Uploads", DateTime.Now.ToLongTimeString() +
                                            " - New file created: " + fileInfo.FullName);

                    // create call and queue for processing by thread pool
                    WaitCallback dispatcherCall = new WaitCallback(LoadAndRunFileProcessors);
                    ThreadPool.QueueUserWorkItem(dispatcherCall, fileInfo.FullName);
                }

            }
            catch (System.Exception ex)
            {
                EventLog.WriteEntry("Process Specimen Operation File Uploads",
                                        "Error: " + ex.Message,
                                        EventLogEntryType.Error);
            }
        }

        private static void LoadAndRunFileProcessors(object state)
        {
            string file = (string)state;

            if (file != null && file.Length > 0)
            {
                char[] caMatch = { '\\' };
                string filePath = file.Substring(0, file.LastIndexOfAny(caMatch));
                string mutexName = filePath.Replace("\\", ":");

                /// Create mutex to serialize access to the directory the file is under.
                /// This in turn serializes processing of multiple files in the same
                /// directory.
                /// 
                Mutex serializingMutex = new Mutex(false, mutexName);
                /// Try to gain access to named mutex or wait until mutex is released.
                serializingMutex.WaitOne();


                try
                {
                    EventLog.WriteEntry("Document Processing File Uploads", DateTime.Now.ToLongTimeString() +
                                            " - Start Processing File: " + file);


                    // Load ABBYY FineReader Engine
                    //displayMessage("Initializing Engine...");
                    loadEngine();

                    // Process with ABBYY FineReader Engine
                    processWithEngine(file);

                    // Unload ABBYY FineReader Engine
                    //displayMessage("Deinitializing Engine...");
                    unloadEngine();

                }
                catch (System.Exception ex)
                {
                    EventLog.WriteEntry("Document Processing Error", DateTime.Now.ToLongTimeString() +
                                           " - " + ex.Message);

                }
                finally
                {
                    File.Move(file, ConfigurationManager.AppSettings["ArchiveDirectory"]  + Path.GetFileName(file));

                    serializingMutex.ReleaseMutex();
                    EventLog.WriteEntry("Document Processing File Uploads", DateTime.Now.ToLongTimeString() +
                                            " - End Processing File: " + file);
                }
            }
        }

        #region AbbyCode

        private static void loadEngine()
        {
            if (engineLoader == null)
            {
                engineLoader = new EngineLoader.EngineLoader();
            }
        }

        private static void loadProfile()
        {
            engineLoader.Engine.LoadPredefinedProfile("DocumentConversion_Accuracy");
            // Possible profile names are:
            //   "DocumentConversion_Accuracy", "DocumentConversion_Speed",
            //   "DocumentArchiving_Accuracy", "DocumentArchiving_Speed",
            //   "BookArchiving_Accuracy", "BookArchiving_Speed",
            //   "TextExtraction_Accuracy", "TextExtraction_Speed",
            //   "FieldLevelRecognition",
            //   "BarcodeRecognition",
            //   "Version9Compatibility"
            //   "Default"
        }

        private static void unloadEngine()
        {
            if (engineLoader != null)
            {
                engineLoader.Dispose();
                engineLoader = null;
            }
        }

        private static void setupFREngine()
        {
            loadProfile();

            //engineLoader.Engine.ParentWindow = this.Handle.ToInt32();
            //engineLoader.Engine.ApplicationTitle = this.Text;
        }

        private static void processImage(string fileName)
        {
            // Create document
            FREngine.FRDocument document = engineLoader.Engine.CreateFRDocument();

            try
            {
                //string imagePath = Path.Combine( FreConfig.GetSamplesFolder(), @"SampleImages\Demo.tif" );
                string imagePath = fileName;
                int pageId = Convert.ToInt32(Path.GetFileNameWithoutExtension(fileName));
                document.AddImageFile(imagePath, null, null);
                document.Process(null, null, null);
                processDocument(document, pageId);
            }
            catch (Exception error)
            {
                //MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close document
                document.Close();
            }
        }

        private static void processWithEngine(string fileName)
        {
            try
            {
                // Setup FREngine
                setupFREngine();

                // Process sample image
                processImage(fileName);
            }
            catch (Exception error)
            {
              //  MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void processDocument(FRDocument pDoc, int pageId)
        {
            //SqlCommand oCmd = new SqlCommand();

            try
            {
                //oCmd.Connection = DAO.getConnection(Configuration.Keys.SQLDBConnStr);
                //oCmd.Connection.Open();
                //oCmd.Transaction = oCmd.Connection.BeginTransaction();

                //string uPK = Guid.NewGuid().ToString();

                for (int iPgIndex = 0; iPgIndex < pDoc.Pages.Count; iPgIndex++)
                {
                    FRPage oPage = pDoc.Pages[iPgIndex];
                    for (int iBlkIndex = 0; iBlkIndex < oPage.Layout.Blocks.Count; iBlkIndex++)
                    {
                        IBlock oBlock = oPage.Layout.Blocks[iBlkIndex];

                        processBlock(oBlock, false, pageId, iBlkIndex + 1, null);

                        //if (oBlock.GetAsTextBlock() != null)
                        //{
                        //  IParagraphs oPars = oBlock.GetAsTextBlock().Text.Paragraphs;

                        //  for (int iParIndex = 0; iParIndex < oPars.Count; iParIndex++)
                        //  {
                        //    //for (int iCharIndex = 0; iCharIndex < oPars[iParIndex].Length; iCharIndex++)
                        //    //{
                        //    //  CharParams oCP = engineLoader.Engine.CreateCharParams();
                        //    //  oPars[iParIndex].GetCharParams(iCharIndex, oCP);

                        //    //  DocChar oDC = new DocChar();
                        //    //  oDC.Bottom = oCP.Bottom;

                        //    //  //TODO:  figure out the correct want to get the character value
                        //    //  oDC.Character = oPars[iParIndex].Range(oCP.Left, oCP.Right).ToCharArray()[0];

                        //    //  //oDC.Character = oPars[iParIndex].GetWordRecognitionVariants(iCharIndex).Application.cr

                        //    //  oDC.FontName = oCP.FontName;
                        //    //  oDC.FontSize = oCP.FontSize;
                        //    //  oDC.IsBold = oCP.IsBold;
                        //    //  oDC.IsItalic = oCP.IsItalic;
                        //    //  oDC.IsProofed = oCP.IsProofed;
                        //    //  oDC.IsSubscript = oCP.IsSubscript;
                        //    //  oDC.IsSuperscript = oCP.IsSuperscript;
                        //    //  oDC.IsSuspicious = oCP.IsSuspicious;
                        //    //  oDC.IsUnderlined = oCP.IsUnderlined;
                        //    //  oDC.IsWordStart = oCP.IsWordStart;
                        //    //  oDC.Left = oCP.Left;
                        //    //  oDC.Right = oCP.Right;
                        //    //  oDC.Spacing = oCP.Spacing;
                        //    //  oDC.Top = oCP.Top;

                        //    //  DAO.insertBWRecord(oCmd, oDC, uPK);
                        //    //}
                        //  }
                        //}

                    }
                }

                //oCmd.Transaction.Commit();
            }
            catch (Exception ex)
            {
                //oCmd.Transaction.Rollback();
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //oCmd.Dispose();
                //oCmd = null;
            }

        }
        private static void processBlock(IBlock pBlock, bool tableBlock, int pageId, int blockNumber, int? cellNumber)
        {
            if (pBlock.Type == BlockTypeEnum.BT_Text)
            {
                processTextBlock(pBlock.GetAsTextBlock(), tableBlock, pageId, blockNumber, cellNumber);
            }
            else if (pBlock.Type == BlockTypeEnum.BT_Table)
            {
                processTableBlock(pBlock.GetAsTableBlock(), pageId, blockNumber);
            }
        }
        private static void processTextBlock(TextBlock pTextBlock, bool tableBlock, int pageId, int blockNumber, int? cellNumber)
        {
            processText(pTextBlock.Text, pageId, tableBlock, true, blockNumber, cellNumber);
        }
        private static void processTableBlock(TableBlock pTableBlock, int pageId, int blockNumber)
        {
            for (int index = 0; index < pTableBlock.Cells.Count; index++)
            {
                processBlock(pTableBlock.Cells[index].Block, true, pageId, blockNumber, index + 1);
            }
        }
        private static void processText(Text pText, int pageId, bool tableBlock, bool textBlock, int blockNumber, int? cellNumber)
        {
            Paragraphs paragraphs = pText.Paragraphs;
            for (int index = 0; index < paragraphs.Count; index++)
            {
                processWordsOfParagraph(paragraphs[index], pageId, index, tableBlock, textBlock, blockNumber, cellNumber);
            }
        }

        static bool isUnrecognizedSymbol(char symbol)
        {
            //it is special constant used by FREngine recogniser
            return (symbol == 0x005E);
        }

        static void processWordsOfParagraph(Paragraph pParagraph, int pageId, int index, bool tableBlock, bool textBlock, int blockNumber, int? cellNumber)
        {

            try
            {
                string uPK = Guid.NewGuid().ToString();

                StringBuilder sbWord = new StringBuilder();
                DocWord oDW = new DocWord();
                int wordCounter = 1;
                for (int iCharIndex = 0; iCharIndex < pParagraph.Text.Length; iCharIndex++)
                {
                    Char cLetter = pParagraph.Text[iCharIndex];
                    if (!isUnrecognizedSymbol(cLetter))
                    {

                        CharParams oCP = engineLoader.Engine.CreateCharParams();
                        pParagraph.GetCharParams(iCharIndex, oCP);

                        if (oCP.IsWordStart)
                        {
                            sbWord = new StringBuilder(cLetter.ToString());
                            oDW = new DocWord();
                            oDW.FontName = oCP.FontName;
                            oDW.FontSize = oCP.FontSize;
                            oDW.IsBold = oCP.IsBold;
                            oDW.IsItalic = oCP.IsItalic;
                            oDW.IsProofed = oCP.IsProofed;
                            oDW.IsSubscript = oCP.IsSubscript;
                            oDW.IsSuperscript = oCP.IsSuperscript;
                            oDW.IsSuspicious = oCP.IsSuspicious;
                            oDW.IsUnderlined = oCP.IsUnderlined;
                            oDW.IsSmallCaps = oCP.IsSmallCaps;
                            oDW.Left = oCP.Left;
                            oDW.Spacing = oCP.Spacing;
                            oDW.Top = oCP.Top;
                            oDW.IsSmallCaps = oCP.IsSmallCaps;
                            //oDW.IsWordFromDictionary = pParagraph.Words[index].IsWordFromDictionary;
                            //oDW.WordConfidence = oWRVs.Item(0).WordConfidence;
                        }
                        else
                        {
                            if (cLetter != ' ')
                            {
                                if ((int)cLetter == 8232)
                                {
                                    oDW.Word = sbWord.ToString().Trim();
                                    DAO.insertBWRecord(oDW, uPK, pageId, index, wordCounter, tableBlock, textBlock, blockNumber, cellNumber);
                                    wordCounter++;
                                }
                                else
                                {
                                    sbWord.Append(cLetter.ToString());

                                    oDW.Bottom = oCP.Bottom;
                                    oDW.Right = oCP.Right;
                                    if (iCharIndex + 1 == pParagraph.Text.Length)
                                    {
                                        oDW.Word = sbWord.ToString().Trim();
                                        DAO.insertBWRecord(oDW, uPK, pageId, index, wordCounter, tableBlock, textBlock, blockNumber, cellNumber);
                                        wordCounter++;
                                    }
                                }
                            }
                            else
                            {
                                if (sbWord.Length > 0)
                                {
                                    oDW.Word = sbWord.ToString().Trim();
                                    DAO.insertBWRecord(oDW, uPK, pageId, index, wordCounter, tableBlock, textBlock, blockNumber, cellNumber);
                                    wordCounter++;
                                }
                            }
                        }



                    }


                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
            }


        }

        #endregion
    }
}
