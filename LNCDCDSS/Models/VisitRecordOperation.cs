﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.Validation;
namespace LNCDCDSS.Models
{
    public class VisitRecordOperation
    {
        LNCDDataModelContainer context = new LNCDDataModelContainer();
        public void InsertPatPhysicaExam(PatPhysicalExam PExam, string ID)
        {
            try
            {
                PatBasicInfor pt = context.PatBasicInforSet.Find(ID);
                //  pt.PatPhysicalExam = PExam;
                //   pt.PatPhysicalExam.B1=PExam.B1;
                ObjectMapper.CopyProperties(PExam, pt.PatPhysicalExam);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string a = e.InnerException.Message;
            }
        }
        public void InsertPatDisease(PatDisease pdisease, string ID)
        {
            try
            {
                PatBasicInfor pt = context.PatBasicInforSet.Find(ID);
                pdisease.PatBasicInforId = "123";
                pdisease.PatBasicInforId = ID;
                pdisease.Id = pt.PatDisease.Id;
                ObjectMapper.CopyProperties(pdisease, pt.PatDisease);
                //  pt.PatDisease = pdisease;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string a = e.InnerException.Message;
            }
        }
        public void InsertPatLabExam(PatLabExam Plab, string ID)
        {
            try
            {
                PatBasicInfor pt = context.PatBasicInforSet.Find(ID);

                pt.VisitRecord.Last().PatLabExam = Plab;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string a = e.InnerException.Message;
            }
        }
        public void InsertPatotherTest(PatOtherTest Ptext, string ID)
        {
            try
            {
                PatBasicInfor pt = context.PatBasicInforSet.Find(ID);
                pt.VisitRecord.Last().PatOtherTest = Ptext;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string a = e.InnerException.Message;
            }
        }
        public void InsertPatADL(PatADL pl, string ID)
        {
            try
            {
                PatBasicInfor pt = context.PatBasicInforSet.Find(ID);
                pt.VisitRecord.Last().PatADL = pl;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string a = e.InnerException.Message;
            }
        }
        public void InsertPatMMSE(PatMMSE pmm, string ID)
        {
            try
            {
                PatBasicInfor pt = context.PatBasicInforSet.Find(ID);
                pt.VisitRecord.Last().PatMMSE = pmm;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string a = e.InnerException.Message;
            }
        }
        public void InsertPatMoca(PatMoCA pm, string ID)
        {
            try
            {
                PatBasicInfor pt = context.PatBasicInforSet.Find(ID);
                pt.VisitRecord.Last().PatMoCA = pm;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string a = e.InnerException.Message;
            }
        }
        public void InsertPatRecentDrug(List<PatRecentDrug> PRdrug, string ID)
        {
            try
            {
                PatBasicInfor pt = context.PatBasicInforSet.Find(ID);
                pt.VisitRecord.Last().PatRecentDrug = PRdrug;
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public bool UpdateVisitRecord(VisitRecord vsr, string ID)
        {
            PatBasicInfor pt = context.PatBasicInforSet.Find(ID);
            pt.VisitRecord.Last().CDSSDiagnosis = vsr.CDSSDiagnosis;
            pt.VisitRecord.Last().DiagnosisiResult = vsr.DiagnosisiResult;
            pt.VisitRecord.Last().RecordNote = vsr.RecordNote;
            context.SaveChanges();
            return true;

        }

        public List<string> GetVisitContent(string RecordID)
        {
            int recordId = int.Parse(RecordID);
            VisitRecord vd = context.VisitRecordSet.Find(recordId);
            var patID = vd.PatBasicInforId;
           // SimpleADdata sa=conte
            List<string> conttext = new List<string>();
            // string test = vd.PatADL.Total + vd.PatMMSE.Total;

            try
            {
                string test = "";
                if (vd.SimpleADdata != null)
                {
                    test += "MMSE-M8=" + vd.SimpleADdata.SAD1;
                    test += ";IADL=" + vd.SimpleADdata.SAD2;
                    test += ";词表学习1=" + vd.SimpleADdata.SAD3;
                    test += ";MoCA注意1=" + vd.SimpleADdata.SAD11;
                    test += ";C3A图形复制=" + vd.SimpleADdata.SAD61;
                    test += ";词表学习2=" + vd.SimpleADdata.SAD4;
                    test += ";MoCA注意2=" + vd.SimpleADdata.SAD12;
                    test += ";C3B即刻回忆=" + vd.SimpleADdata.SAD62;
                    test += ";词表学习3=" + vd.SimpleADdata.SAD5;
                    test += ";MoCA注意3=" + vd.SimpleADdata.SAD13;                   
                    test += ";C3C延迟回忆=" + vd.SimpleADdata.SAD63;
                    test += ";词表学习4=" + vd.SimpleADdata.SAD14;
                    test += ";MoCA命名=" + vd.SimpleADdata.SAD10;
                    test += ";MoCA连线=" + vd.SimpleADdata.SAD7;
                    test += ";MoCA画钟表=" + vd.SimpleADdata.SAD9;
                    test += ";MoCA复制立方体=" + vd.SimpleADdata.SAD8;
                    
                }
                else
                {
                    test += "MMSE-M8=";
                    test += ";IADL=";
                    test += ";词表学习1=";
                    test += ";MoCA注意1=" ;
                    test += ";C3A图形复制=";
                    test += ";词表学习2=" ;
                    test += ";MoCA注意2=";
                    test += ";C3B即刻回忆=";
                    test += ";词表学习3=";
                    test += ";MoCA注意3=";                   
                    test += ";C3C延迟回忆=";
                    test += ";词表学习4=" ;
                    test += ";MoCA命名=";
                    test += ";MoCA连线=";
                    test += ";MoCA画钟表=";
                    test += ";MoCA复制立方体=";
                }
                conttext.Add(test);
                conttext.Add(vd.RecordNote);
                conttext.Add(vd.DiagnosisiResult);
            }
            catch (Exception e)
            { }
            return conttext;
        }
        public string[] GetExamContent(string PatID, string RecordID)
        {
            int recordId = int.Parse(RecordID);
            PatBasicInfor pt = context.PatBasicInforSet.Find(PatID);
            VisitRecord vd = context.VisitRecordSet.Find(recordId);
            string[] content = new string[17];
            // string test = vd.PatADL.Total + vd.PatMMSE.Total;
            for (int i = 0; i < 17; i++)
            {
                content[i] = "";
            }
            try
            {

                if (vd.PatMMSE != null)
                {
                    content[0] = vd.PatMMSE.Total;
                }

                if (vd.PatMoCA != null)
                {
                    content[1] = vd.PatMoCA.Total;
                }
                if (vd.PatADL != null)
                {
                    content[2] = vd.PatADL.Total;
                }

                if (vd.PatOtherTest != null)
                {
                    content[3] = vd.PatOtherTest.PatCDR;
                    content[4] = vd.PatOtherTest.PatGDS;
                    content[5] = vd.PatOtherTest.Vocabulary1;
                    content[6] = vd.PatOtherTest.Vocabulary2;
                    content[7] = vd.PatOtherTest.Vocabulary3;
                    content[8] = vd.PatOtherTest.Vocabulary4;
                    content[9] = vd.PatOtherTest.VocabularyAnalyse1;
                    content[10] = vd.PatOtherTest.VocabularyAnalyse2;
                    content[11] = vd.PatOtherTest.Picture1;
                    content[12] = vd.PatOtherTest.Picture2;
                    content[13] = vd.PatOtherTest.Picture3;
                    content[14] = vd.PatOtherTest.ConnectNumber1;
                    content[15] = vd.PatOtherTest.ConnectNumber2;
                }
                if(vd.RecordNote!=null)
                {
                    content[16] = vd.RecordNote;
                }

            }
            catch (Exception e)
            {
            }
            return content;
        }
        public List<PatBasicInfor> GetPat(List<string> Condition)
        {
            List<PatBasicInfor> pat = new List<PatBasicInfor>();
            List<PatBasicInfor> Unormalpat = new List<PatBasicInfor>();
            var pats = from p in context.PatBasicInforSet.ToList()
                       where (string.IsNullOrEmpty(Condition[0]) ? true : p.Name == Condition[0])
                      && (string.IsNullOrEmpty(Condition[1]) ? true : p.Sex == Condition[1])
                      && (string.IsNullOrEmpty(Condition[4]) ? true : p.DoctorAccount.UserName == Condition[4])
                      && (string.IsNullOrEmpty(Condition[2]) ? true : p.VisitRecord.Last().VisitDate == DateTime.Parse(Condition[2]))
                      && (string.IsNullOrEmpty(Condition[3]) ? true : p.VisitRecord.Last().DiagnosisiResult == Condition[3])
                       select p;

            try
            {
                
                foreach (PatBasicInfor pt in pats)
                {if (pt.VisitRecord!=null&&pt.VisitRecord.Count!=0)
                {
                     pat.Add(pt);
                } 
                 else
                {
                    Unormalpat.Add(pt);
                }
                }
                if (string.IsNullOrEmpty(Condition[2]))
                {
                    InsertSort(pat);
                }
                pat.AddRange(Unormalpat);
            }
            catch (Exception e)
            {
                string error = e.Message;

            }
            return pat;
        }
        public List<VisitRecord> GetVistRecord(string PatID)
        {
            PatBasicInfor pt = context.PatBasicInforSet.Find(PatID);
            List<VisitRecord> visit = new List<VisitRecord>();
            foreach (VisitRecord vr in pt.VisitRecord)
            {
                visit.Add(vr);
            }
            visit.Reverse();
            return visit;
        }

        public List<string> LastSpdata(string PatID)
        {
            PatBasicInfor pt = context.PatBasicInforSet.Find(PatID);
            var vr=pt.VisitRecord.First();
            List<string> lastspdata = new List<string>();
           
           
            if (pt.SimpleADdata.Count == 0)
            { 
                SimpleADdata sp = new SimpleADdata();
                sp.SAD1 = "";
                sp.SAD10 = "";
                sp.SAD11 = "";
                sp.SAD12 = "";
                sp.SAD13 = "";
                sp.SAD14 = "";
                sp.SAD2 = "";
                sp.SAD3 = "";
                sp.SAD4 = "";
                sp.SAD5 = "";
                sp.SAD61 = "";
                sp.SAD62 = "";
                sp.SAD63 = "";
                sp.SAD7 = "";
                sp.SAD8 = "";
                sp.SAD9 = "";
                sp.patID = PatID;
                pt.SimpleADdata.Add(sp);
                pt.VisitRecord.Last().SimpleADdata = sp;
                context.SimpleADdataSet.Add(sp);             
                context.SaveChanges();
             }  
            var spl = pt.SimpleADdata.Last();
                lastspdata.Add(spl.SAD1);
                lastspdata.Add(spl.SAD2);
                lastspdata.Add(spl.SAD3);
                lastspdata.Add(spl.SAD4);
                lastspdata.Add(spl.SAD5);
                lastspdata.Add(spl.SAD61);
                lastspdata.Add(spl.SAD62);
                lastspdata.Add(spl.SAD63);
                lastspdata.Add(spl.SAD7);
                lastspdata.Add(spl.SAD8);
                lastspdata.Add(spl.SAD9);
                lastspdata.Add(spl.SAD10);
                lastspdata.Add(spl.SAD11);
                lastspdata.Add(spl.SAD12);
                lastspdata.Add(spl.SAD13);
                lastspdata.Add(spl.SAD14);
                lastspdata.Add(pt.Name);
                lastspdata.Add(pt.Sex);
                lastspdata.Add(pt.Age);
                lastspdata.Add(pt.Phone);
                lastspdata.Add(vr.OutpatientID);
                lastspdata.Add(spl.patID);
             return lastspdata;
        }

        public void AddNewRecord(string PatID)
        {
            PatBasicInfor pt = context.PatBasicInforSet.Find(PatID);

            VisitRecord vr = new VisitRecord();
            if (pt.VisitRecord.Count != 0)
            {
                vr.VisitRecordID = pt.VisitRecord.Last().VisitRecordID;
            }
            else
            {
                vr.VisitRecordID = "1";
            }
            vr.VisitDate = DateTime.Now;
            vr.PatBasicInforId = PatID;
            pt.VisitRecord.Add(vr);
            context.SaveChanges();
        }
        public bool DeleteRecord(string PatID, string RecordID)
        {
            try
            {

                var record = from p in context.VisitRecordSet.ToList()
                             where (p.PatBasicInfor.Id == PatID) && (p.Id == int.Parse(RecordID))
                             select p;
                VisitRecord r = record.First();
                if (r.PatADL != null)
                {
                    context.PatADLSet.Remove(r.PatADL);
                }
                if (r.PatMMSE != null)
                {
                    context.PatMMSESet.Remove(r.PatMMSE);
                }
                if (r.PatMoCA != null)
                {
                    context.PatMoCASet.Remove(r.PatMoCA);
                }
                if (r.PatOtherTest != null)
                {
                    context.PatOtherTestSet.Remove(r.PatOtherTest);
                }
                if (r.PatLabExam != null)
                {
                    context.PatLabExamSet.Remove(r.PatLabExam);
                }
                if (r.PatRecentDrug.Count != 0)
                {
                    foreach (PatRecentDrug rd in r.PatRecentDrug)
                    {
                        foreach (Drug d in rd.Drug)
                        {
                            context.DrugSet.Remove(d);
                        }

                        context.PatRecentDrugSet.Remove(rd);
                    }

                }
                context.VisitRecordSet.Remove(r);
                context.SaveChanges();
                return true;
            }
            catch (System.Exception e)
            {
                return false;

            }
        }
        public bool SaveContinueRecord(string PatID, string RecordID, VisitData visitdata)
        {
            try
            {

                int recordId = int.Parse(RecordID);
                VisitRecord vd = context.VisitRecordSet.Find(recordId);
                if (vd.PatADL == null)
                {
                    vd.PatADL = visitdata.pal;
                }
                else if (vd.PatADL.Total == "")
                {
                    ObjectMapper.CopyValueProperties(visitdata.pal, vd.PatADL);
                }
                if (vd.PatMMSE == null)
                {
                    vd.PatMMSE = visitdata.pme;
                }
                else if (vd.PatMMSE.Total == "")
                {
                    ObjectMapper.CopyValueProperties(visitdata.pme, vd.PatMMSE);
                }
                if (vd.PatMoCA == null)
                {
                    vd.PatMoCA = visitdata.pma;
                }
                else if (vd.PatMoCA.Total == "")
                {
                    ObjectMapper.CopyValueProperties(visitdata.pma, vd.PatMoCA);
                }
                if (vd.PatOtherTest == null)
                {
                    vd.PatOtherTest = visitdata.pot;
                }
                else
                {
                    if (vd.PatOtherTest.Vocabulary1 == "")
                    {
                        vd.PatOtherTest.Vocabulary1 = visitdata.pot.Vocabulary1;
                    }
                    if (vd.PatOtherTest.Vocabulary2 == "")
                    {
                        vd.PatOtherTest.Vocabulary2 = visitdata.pot.Vocabulary2;
                    }
                    if (vd.PatOtherTest.Vocabulary3 == "")
                    {
                        vd.PatOtherTest.Vocabulary3 = visitdata.pot.Vocabulary3;
                    }
                    if (vd.PatOtherTest.Vocabulary4 == "")
                    {
                        vd.PatOtherTest.Vocabulary4 = visitdata.pot.Vocabulary4;
                    }
                    if (vd.PatOtherTest.ConnectNumber1 == "")
                    {
                        vd.PatOtherTest.ConnectNumber1 = visitdata.pot.ConnectNumber1;
                    }
                    if (vd.PatOtherTest.ConnectNumber2 == "")
                    {
                        vd.PatOtherTest.ConnectNumber2 = visitdata.pot.ConnectNumber2;
                    }
                    if (vd.PatOtherTest.PatCDR == "")
                    {
                        vd.PatOtherTest.PatCDR = visitdata.pot.PatCDR;
                    }
                    if (vd.PatOtherTest.PatGDS == "")
                    {
                        vd.PatOtherTest.PatGDS = visitdata.pot.PatGDS;
                    }
                    if (vd.PatOtherTest.Picture1 == "")
                    {
                        vd.PatOtherTest.Picture1 = visitdata.pot.Picture1;
                    }
                    if (vd.PatOtherTest.Picture2 == "")
                    {
                        vd.PatOtherTest.Picture2 = visitdata.pot.Picture2;
                    }
                    if (vd.PatOtherTest.Picture3 == "")
                    {
                        vd.PatOtherTest.Picture3 = visitdata.pot.Picture3;
                    }
                    if (vd.PatOtherTest.VocabularyAnalyse1 == "")
                    {
                        vd.PatOtherTest.VocabularyAnalyse1 = visitdata.pot.VocabularyAnalyse1;
                    }
                    if (vd.PatOtherTest.VocabularyAnalyse2 == "")
                    {
                        vd.PatOtherTest.VocabularyAnalyse2 = visitdata.pot.VocabularyAnalyse2;
                    }
                   


                }

                vd.CDSSDiagnosis = visitdata.vsr.CDSSDiagnosis;
                vd.DiagnosisiResult = visitdata.vsr.DiagnosisiResult;
                vd.RecordNote = visitdata.vsr.RecordNote;
                context.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }
        public bool CopyContinueRecord(string PatID, string RecordID, VisitData visitdata)
        {
            try
            {

                int recordId = int.Parse(RecordID);
                VisitRecord vd = context.VisitRecordSet.Find(recordId);
                if (vd.PatADL != null && vd.PatADL.Total != "")
                {
                    ObjectMapper.CopyValueProperties(vd.PatADL, visitdata.pal);
                }


                if (vd.PatMMSE != null && vd.PatMMSE.Total != "")
                {
                    ObjectMapper.CopyValueProperties(vd.PatMMSE, visitdata.pme);
                }

                if (vd.PatMoCA != null && vd.PatMoCA.Total != "")
                {
                    ObjectMapper.CopyValueProperties(vd.PatMoCA, visitdata.pma);
                }

                if (vd.PatOtherTest != null)
                {
                    if (vd.PatOtherTest.Vocabulary1 != "")
                    {
                        visitdata.pot.Vocabulary1 = vd.PatOtherTest.Vocabulary1;
                    }
                    if (vd.PatOtherTest.Vocabulary2 != "")
                    {
                        visitdata.pot.Vocabulary2 = vd.PatOtherTest.Vocabulary2;
                    }
                    if (vd.PatOtherTest.Vocabulary3 != "")
                    {
                        visitdata.pot.Vocabulary3 = vd.PatOtherTest.Vocabulary3;
                    }
                    if (vd.PatOtherTest.Vocabulary4 != "")
                    {
                        visitdata.pot.Vocabulary4 = vd.PatOtherTest.Vocabulary4;
                    }
                    if (vd.PatOtherTest.ConnectNumber1 != "")
                    {
                        visitdata.pot.ConnectNumber1 = vd.PatOtherTest.ConnectNumber1;
                    }
                    if (vd.PatOtherTest.ConnectNumber2 != "")
                    {
                        visitdata.pot.ConnectNumber2 = vd.PatOtherTest.ConnectNumber2;
                    }
                    if (vd.PatOtherTest.PatCDR != "")
                    {
                        visitdata.pot.PatCDR = vd.PatOtherTest.PatCDR;
                    }
                    if (vd.PatOtherTest.PatGDS != "")
                    {
                        visitdata.pot.PatGDS = vd.PatOtherTest.PatGDS;
                    }
                    if (vd.PatOtherTest.Picture1 != "")
                    {
                        visitdata.pot.Picture1 = vd.PatOtherTest.Picture1;
                    }
                    if (vd.PatOtherTest.Picture2 != "")
                    {
                        visitdata.pot.Picture2 = vd.PatOtherTest.Picture2;
                    }
                    if (vd.PatOtherTest.Picture3 != "")
                    {
                        visitdata.pot.Picture3 = vd.PatOtherTest.Picture3;
                    }
                    if (vd.PatOtherTest.VocabularyAnalyse1 != "")
                    {
                        visitdata.pot.VocabularyAnalyse1 = vd.PatOtherTest.VocabularyAnalyse1;
                    }
                    if (vd.PatOtherTest.VocabularyAnalyse2 != "")
                    {
                        visitdata.pot.VocabularyAnalyse2 = vd.PatOtherTest.VocabularyAnalyse2;
                    }


                }

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }
        public static void InsertSort(List<PatBasicInfor> data)
        {
            var count = data.Count;
            for (int i = 1; i < count; i++)
            {
                var t = data[i].VisitRecord.Last().VisitDate;
                var d = data[i];
                var j = i;
                while (j > 0 && data[j - 1].VisitRecord.Last().VisitDate < t)
                {
                    data[j] = data[j - 1];
                    --j;
                }
                data[j] =d;
            }
        }

        


    }
}
