using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using DomainObjects;
using BusinessServices;

namespace AbbyTest
{
  internal static class DAO
  {
    #region Methods
    internal static SqlConnection getConnection(Configuration.Keys ConnStr)
    {
      return new SqlConnection(Configuration.item(ConnStr).ToString());
    }
    internal static DataSet getDataSet(SqlCommand pCmd)
    {
      SqlDataAdapter adapter = new SqlDataAdapter();
      DataSet dataSet = new DataSet("SelectedData");

      try
      {
        pCmd.CommandTimeout = Configuration.CommandTimeout;

        using (SqlConnection oConn = new SqlConnection(Configuration.SQLDBConnStr.ToString()))
        {
          adapter.SelectCommand = pCmd;
          adapter.SelectCommand.Connection = oConn;
          adapter.Fill(dataSet);
          if (oConn.State == ConnectionState.Open) oConn.Close();
          oConn.Dispose();
        }

        return dataSet;
      }
      catch (Exception ex)
      {
        throw (ex);
      }
      finally
      {
        adapter.Dispose();
        dataSet.Dispose();
        adapter = null;
      }
    }
    internal static int ExecuteNonQuery(SqlCommand pCmd)
    {
      try
      {
        int iRtn = 0;

        pCmd.CommandTimeout = Configuration.CommandTimeout;

        using (SqlConnection oConn = new SqlConnection(Configuration.SQLDBConnStr.ToString()))
        {
          oConn.Open();
          pCmd.Connection = oConn;
          iRtn = pCmd.ExecuteNonQuery();
          if (oConn.State == ConnectionState.Open) oConn.Close();
          oConn.Dispose();
        }

        return iRtn;
      }
      catch (Exception ex)
      {
        throw (ex);
      }

    }
    internal static DataSet getDocuments()
    {
      SqlCommand oCmd = new SqlCommand();
      try
      {
        oCmd.CommandType = CommandType.Text;
        oCmd.CommandText = "SELECT * FROM dBI WHERE lBIOCR=0";
        return getDataSet(oCmd);
      }
      catch (Exception ex)
      {
        throw (ex);
      }
      finally
      {
        oCmd.Dispose();
        oCmd = null;
      }
    }

    internal static void updateProcessedFlag(SqlCommand pCmd, string puBIPk)
    {
      try
      {
        pCmd.CommandType = CommandType.Text;
        pCmd.CommandText = "UPDATE dBI SET lBIOCR=1 WHERE uBIPK=@p_uBIPK";
        pCmd.Parameters.Clear();
        pCmd.Parameters.Add("@p_uBIPK", SqlDbType.UniqueIdentifier).Value = SqlGuid.Parse(puBIPk);
        pCmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw (ex);
      }
    }

    //internal static void insertBWRecord(SqlCommand pCmd, string puBIPk, int piBWOrder, char pcBWChar, int piBWTop, int piBWBtm, int piBWLft, int piBWRgt, int piBWLn, int piBWSpc, float pfBWCnfd)
    //{
    //  StringBuilder sbSQL = new StringBuilder();
    //  try
    //  {
    //    sbSQL.Append("INSERT INTO dBW ");
    //    sbSQL.Append("(uBWPk, uBIPK, iBWOrdr, cBWChar, iBWTop, iBWBtm, iBWLft, iBWRgt, iBWLn, iBWSpc, fBWCnfd) ");
    //    sbSQL.Append("VALUES ");
    //    sbSQL.Append("(NewID(), @p_uBIPK, @p_iBWOrdr, @p_cBWChar, @p_iBWTop, @p_iBWBtm, @p_iBWLft, @p_iBWRgt, @p_iBWLn, @p_iBWSpc, @p_fBWCnfd)");

    //    pCmd.CommandType = CommandType.Text;
    //    pCmd.CommandText = sbSQL.ToString();
    //    pCmd.Parameters.Clear();
    //    pCmd.Parameters.Add("@p_uBIPK", SqlDbType.UniqueIdentifier).Value = SqlGuid.Parse(puBIPk);
    //    pCmd.Parameters.Add("@p_iBWOrdr", SqlDbType.Int).Value = piBWOrder;
    //    pCmd.Parameters.Add("@p_cBWChar", SqlDbType.Char).Value = pcBWChar;
    //    pCmd.Parameters.Add("@p_iBWTop", SqlDbType.Int).Value = piBWTop;
    //    pCmd.Parameters.Add("@p_iBWBtm", SqlDbType.Int).Value = piBWBtm;
    //    pCmd.Parameters.Add("@p_iBWLft", SqlDbType.Int).Value = piBWLft;
    //    pCmd.Parameters.Add("@p_iBWRgt", SqlDbType.Int).Value = piBWRgt;
    //    pCmd.Parameters.Add("@p_iBWLn", SqlDbType.Int).Value = piBWLn;
    //    pCmd.Parameters.Add("@p_iBWSpc", SqlDbType.Int).Value = piBWSpc;
    //    pCmd.Parameters.Add("@p_fBWCnfd", SqlDbType.Float).Value = pfBWCnfd;
    //    pCmd.ExecuteNonQuery();
    //  }
    //  catch (Exception ex)
    //  {
    //    throw (ex);
    //  }
    //  finally
    //  {

    //  }
    //}

    internal static void insertBWRecord(DocWord pWord, string pBIPK, int pageId, int index, int wordCounter, bool tableBlock,  bool textBlock, int blockNumber, int? cellNumber)
    {
      //StringBuilder sbSQL = new StringBuilder();
      try
      {
        //sbSQL.Append("INSERT INTO dBW ");
        //sbSQL.Append("(uBIPK, cBWWrd, iBWTop, iBWLft, iBWBtm, iBWRght, cBWFntNm, iBWFntSz, lBWFntBld, lBWFntItlc, ");
        //sbSQL.Append(" lBWFntUndrLn, lBWFntSbScrpt, lBWFntSprScrpt, lWCSuspcs, lBWPrfd, iBWCnfdnc, lBWDctnry, lBWSmlCps) ");
        //sbSQL.Append("VALUES ");
        //sbSQL.Append("(@p_uBIPK, @p_cBWWrd, @p_iBWTop, @p_iBWLft, @p_iBWBtm, @p_iBWRght, @p_cBWFntNm, @p_iBWFntSz, @p_lBWFntBld, @p_lBWFntItlc, ");
        //sbSQL.Append(" @p_lBWFntUndrLn, @p_lBWFntSbScrpt, @p_lBWFntSprScrpt, @p_lBWSuspcs, @p_lBWPrfd, @p_iWrdCnfdnc, @p_lWrdFrmDctnry, @p_lBWSmlCps) ");
        
        //pCmd.CommandType = CommandType.Text;
        //pCmd.CommandText = sbSQL.ToString();
        //pCmd.Parameters.Clear();
        //pCmd.Parameters.Add("@p_uBIPK", SqlDbType.UniqueIdentifier).Value = SqlGuid.Parse(pBIPK);
        //pCmd.Parameters.Add("@p_cBWWrd", SqlDbType.VarChar).Value = pWord.Word;
        //pCmd.Parameters.Add("@p_iBWTop", SqlDbType.Int).Value = pWord.Top;
        //pCmd.Parameters.Add("@p_iBWLft", SqlDbType.Int).Value = pWord.Left;
        //pCmd.Parameters.Add("@p_iBWBtm", SqlDbType.Int).Value = pWord.Bottom;
        //pCmd.Parameters.Add("@p_iBWRght", SqlDbType.Int).Value = pWord.Right;
        //pCmd.Parameters.Add("@p_cBWFntNm", SqlDbType.Char).Value = pWord.FontName;
        //pCmd.Parameters.Add("@p_iBWFntSz", SqlDbType.Int).Value = pWord.FontSize;
        //pCmd.Parameters.Add("@p_lBWFntBld", SqlDbType.Bit).Value = pWord.IsBold ? 1: 0;
        //pCmd.Parameters.Add("@p_lBWFntItlc", SqlDbType.Bit).Value = pWord.IsItalic ? 1 : 0;
        //pCmd.Parameters.Add("@p_lBWFntUndrLn", SqlDbType.Bit).Value = pWord.IsUnderlined ? 1 :0 ;
        //pCmd.Parameters.Add("@p_lBWFntSbScrpt", SqlDbType.Bit).Value = pWord.IsSubscript ? 1 : 0;
        //pCmd.Parameters.Add("@p_lBWFntSprScrpt", SqlDbType.Bit).Value = pWord.IsSuperscript ? 1 : 0;
        //pCmd.Parameters.Add("@p_lBWSuspcs", SqlDbType.Bit).Value = pWord.IsSuspicious ? 1 :0;
        //pCmd.Parameters.Add("@p_lBWPrfd", SqlDbType.Bit).Value = pWord.IsProofed ? 1 : 0;
        //pCmd.Parameters.Add("@p_iWrdCnfdnc", SqlDbType.Int).Value = pWord.WordConfidence;
        //pCmd.Parameters.Add("@p_lWrdFrmDctnry", SqlDbType.Bit).Value = pWord.IsWordFromDictionary ? 1 : 0;
        //pCmd.Parameters.Add("@p_lBWSmlCps", SqlDbType.Bit).Value = pWord.IsSmallCaps ? 1 : 0;

        //pCmd.ExecuteNonQuery();


        BatchWord batchWord = new BatchWord();
        batchWord.BatchPage = FileFacade.GetBatchPageById(pageId);
        batchWord.Bottom = pWord.Bottom;
        batchWord.FontBold = pWord.IsBold;
        batchWord.FontItalic = pWord.IsItalic;
        batchWord.FontName = pWord.FontName;
        batchWord.FontSize = pWord.FontSize;
        batchWord.FontSubscript = pWord.IsSubscript;
        batchWord.FontSuperscript = pWord.IsSuperscript;
        batchWord.FontSuspicious = pWord.IsSuspicious;
        batchWord.FontUnderline = pWord.IsUnderlined;
        batchWord.FromDictionary = pWord.IsWordFromDictionary;
        batchWord.Left = pWord.Left;
        batchWord.Proofed = pWord.IsProofed;
        batchWord.Right = pWord.Right;
        batchWord.Top = pWord.Top;
        batchWord.Word = pWord.Word;
        batchWord.SmallCaps = pWord.IsSmallCaps;
        batchWord.ParagraphNumber = index + 1;
        batchWord.WordOrder = wordCounter;
        batchWord.IsTextBlock = textBlock;
        batchWord.CellNumber = cellNumber;
        batchWord.BlockNumber = blockNumber;
        batchWord.IsTableBlock = tableBlock;

          FileFacade.SaveBatchWord(batchWord, new Guid("4B1F04FE-994B-43DD-9B28-544ABA885C0C"));
        //batchWord.WordConfidence = pWord.WordConfidence;

      }
      catch (Exception ex)
      {
          EventLog.WriteEntry("Document Processing Error", ex.InnerException.Message.ToString());
        throw (ex);
      }
      finally
      {

      }
    }


    #endregion
  }
}
