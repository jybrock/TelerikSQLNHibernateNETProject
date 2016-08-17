using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using DomainObjects;

namespace BusinessServices
{
    public partial class FileFacade
    {
        static PersistanceModule _PersistanceModule = null;

        static FileFacade()
        {
            _PersistanceModule = new PersistanceModule();
        }

        #region BatchFile

        public static BatchFile[] GetAllBatchFile()
        {
            BatchFile[] BatchFiles = null;

            try
            {
                BatchFiles = BatchFile.FindAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed getting BatchFiles", ex);
            }

            return BatchFiles;
        }

        public static BatchFile GetBatchFileById(int id)
        {
            BatchFile BatchFile = null;

            try
            {
                BatchFile = BatchFile.TryFind(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed getting BatchFile: " + id, ex);
            }

            return BatchFile;
        }

        public static int GetBatchFileCount()
        {
            return ActiveRecordMediator<BatchFile>.Count();
        }

        public static int SaveBatchFile(BatchFile batchFile, Guid userId)
        {
            int retVal = 0;

            try
            {
                retVal = batchFile.Save(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed saving batchFile", ex);
            }

            return retVal;
        }

        #endregion

        #region BatchWord

        public static BatchWord GetBatchWordById(int id)
        {
            BatchWord BatchWord = null;

            try
            {
                BatchWord = BatchWord.TryFind(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed getting BatchWord: " + id, ex);
            }

            return BatchWord;
        }

        public static int GetBatchWordCount()
        {
            return ActiveRecordMediator<BatchWord>.Count();
        }

        public static int SaveBatchWord(BatchWord batchWord, Guid userId)
        {
            int retVal = 0;

            try
            {
                retVal = batchWord.Save(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed saving batchWord", ex);
            }

            return retVal;
        }

        #endregion

        #region BatchPage

        public static BatchPage GetBatchPageById(int id)
        {
            BatchPage BatchPage = null;

            try
            {
                BatchPage = BatchPage.TryFind(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed getting BatchPage: " + id, ex);
            }

            return BatchPage;
        }

        public static int GetBatchPageCount()
        {
            return ActiveRecordMediator<BatchPage>.Count();
        }

        public static int SaveBatchPage(BatchPage batchPage, Guid userId)
        {
            int retVal = 0;

            try
            {
                retVal = batchPage.Save(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed saving batchPage", ex);
            }

            return retVal;
        }

        #endregion

        #region BatchFileDescription

        public static BatchFileDescription GetBatchFileDescriptionById(int id)
        {
            BatchFileDescription BatchFileDescription = null;

            try
            {
                BatchFileDescription = BatchFileDescription.TryFind(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed getting BatchFileDescription: " + id, ex);
            }

            return BatchFileDescription;
        }

        public static BatchFileDescription[] GetBatchFileDescriptionByBatchFileId(int batchFileId)
        {
            BatchFileDescription[] BatchFileDescriptions = null;

            try
            {
                BatchFileDescriptions = BatchFileDescription.FindByBatchFileId(batchFileId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed getting BatchFileDescription: " , ex);
            }

            return BatchFileDescriptions;
        }

        public static int GetBatchFileDescriptionCount()
        {
            return ActiveRecordMediator<BatchFileDescription>.Count();
        }

        public static int SaveBatchFileDescription(BatchFileDescription batchFileDescription, Guid userId)
        {
            int retVal = 0;

            try
            {
                retVal = batchFileDescription.Save(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed saving batchFileDescription", ex);
            }

            return retVal;
        }

        #endregion

        #region Reference

        public static Reference GetReferenceById(int id)
        {
            Reference Reference = null;

            try
            {
                Reference = Reference.TryFind(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed getting Reference: " + id, ex);
            }

            return Reference;
        }

        public static int GetReferenceCount()
        {
            return ActiveRecordMediator<Reference>.Count();
        }

        public static int SaveReference(Reference reference, Guid userId)
        {
            int retVal = 0;

            try
            {
                retVal = reference.Save(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed saving reference", ex);
            }

            return retVal;
        }

        #endregion
    }
}
