using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LNCDCDSS.Models;
using System.Data.Entity.Validation;
using LNCDCDSS.Filters;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace LNCDCDSS.Controllers
{
       public class DiagnosisController : Controller
       {
         LNCDDataModelContainer DContainer = new LNCDDataModelContainer();
        public ActionResult Index(string ID)
        {      
            this.TempData["PatID"] = ID;
            return View("Index");
        }
        
        [HttpPost]
        public JsonResult Save(string dia,string Abr,string ID)
        {

            try
            {
                string Readme = Abr;
                string docDia = dia;
                PatBasicInfor pt = DContainer.PatBasicInforSet.Find(ID);
                VisitRecord vr = new VisitRecord();
                pt.VisitRecord.Last().RecordNote = Readme;//病人自述
                pt.VisitRecord.Last().DiagnosisiResult = docDia;
                DContainer.SaveChanges();

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
           
            return this.Json(new { OK = true, Message = "保存成功" });
        
            //return RedirectToAction("Index", "EnterPatInfor");
        }

        public ActionResult Predata()
        {
            VisitRecordOperation vro = new VisitRecordOperation();
            string PatID = this.TempData["PatID"].ToString();
            var lastlist = vro.LastSpdata(PatID);
            return Json(lastlist, JsonRequestBehavior.AllowGet);
        }
[HttpPost]
        public string Index(SimpleADdata spdata,string ID)
        {
            string strResult = null;
            double dProbalily = 0.0f;
            spdata.patID = ID;
            PatBasicInfor pt = DContainer.PatBasicInforSet.Find(ID);

            try
            {
                pt.SimpleADdata.Add(spdata);
                pt.VisitRecord.Last().SimpleADdata = spdata;
                DContainer.SimpleADdataSet.Add(spdata);
                DContainer.SaveChanges();
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

            try
            {           
              VisitRecord vr = new VisitRecord();
              WebReference.InputData InputDataValue = new WebReference.InputData();
              InputDataValue.readingcomprehension = System.Convert.ToDouble(spdata.SAD1);//M8
              InputDataValue.Visualspaceandexecutiveability = System.Convert.ToDouble(spdata.SAD7) + System.Convert.ToDouble(spdata.SAD8) + System.Convert.ToDouble(spdata.SAD9);//视空间与执行能力
              InputDataValue.naming = System.Convert.ToDouble(spdata.SAD10);//命名
              InputDataValue.attention = System.Convert.ToDouble(spdata.SAD11) + System.Convert.ToDouble(spdata.SAD12) + System.Convert.ToDouble(spdata.SAD13);//MC4
              InputDataValue.grippingability = System.Convert.ToDouble(spdata.SAD2);//IADL
              InputDataValue.wordaverage = (System.Convert.ToDouble(spdata.SAD3) + System.Convert.ToDouble(spdata.SAD4) + System.Convert.ToDouble(spdata.SAD5)) / 3;
              InputDataValue.worddelayrecall = System.Convert.ToDouble(spdata.SAD14);//词表学习的最后一遍  忘记做了
              InputDataValue.graphcopy = System.Convert.ToDouble(spdata.SAD61);
              InputDataValue.graphimmediaterecall = System.Convert.ToDouble(spdata.SAD62);
              InputDataValue.graphdelayrecall = System.Convert.ToDouble(spdata.SAD63);
              WebReference.InferenceService b = new WebReference.InferenceService();
              b.DoInference(InputDataValue, ref strResult, ref dProbalily);

              if ("AD" == strResult)
                  strResult = "阿尔兹海默症，具体类型待定，请结合病史";
              else if ("MCI" == strResult)
                  strResult = "轻度认知功能障碍，具体类型待定，请结合病史";
              else if ("Normal" == strResult)
                  strResult = "正常";
              string Readme = Request.Form["病情自述"];
              pt.VisitRecord.Last().CDSSDiagnosis = strResult+"              相似度:" + (dProbalily * 100).ToString("0.00") + "%" ;
                DContainer.SaveChanges();
            }

            catch (Exception e)
            {
             //   return this.Json(new { OK = false, Message = e.Message + "推理出错" });
              //  return new JavaScriptSerializer().Serialize(new[] { "false",e.Message + "推理出错" });
                return string.Format("false, '{0}'推理出错",e.Message);
            }
          //  var dia = pt.VisitRecord.Last();
           // return this.Json(dia, JsonRequestBehavior.AllowGet);
          //  return new JavaScriptSerializer().Serialize(new[] {strResult + "              相似度:" + (dProbalily * 100).ToString("0.00") + "%" });
            return string.Format("{0}    相似度：{1}", strResult, (dProbalily * 100).ToString("0.00") + "%");        
}
        //[HttpPost]
        //public JsonResult ContinueCDSSdiagnosis()
        //{
        //    string strResult = null;
        //    double dProbalily = 0.0f;
        //    try
        //    {

        //        string jsonStr = Request.Params["postjson"];
        //        VisitData obj = JsonHelper.JsonDeserialize<VisitData>(jsonStr);//jsonStr.FromJsonTo<VisitData>();
        //        WebReference.InputData InputDataValue = new WebReference.InputData();
        //        string PatID = this.TempData["PatID"].ToString();
        //        string VisitID = this.TempData["ContinueVisitID"].ToString();
        //        this.TempData["PatID"] = PatID;
        //        this.TempData["ContinueVisitID"] = VisitID;
        //        VisitRecordOperation vr = new VisitRecordOperation();
        //        vr.CopyContinueRecord(PatID, VisitID, obj);
        //        if (obj.pme.M1 != "")
        //        {
        //            InputDataValue.timeorientation = System.Convert.ToDouble(obj.pme.M1); //M1
        //        }
        //        if (obj.pme.M2 != "")
        //        {
        //            InputDataValue.placeorientation = System.Convert.ToDouble(obj.pme.M2); //M2
        //        }
        //        if (obj.pme.M3 != "")
        //        {
        //            InputDataValue.Languageimmediaterecall = System.Convert.ToDouble(obj.pme.M3); //M3
        //        }
        //        if (obj.pme.M4 != "")
        //        {
        //            InputDataValue.Attentionandcalculation = System.Convert.ToDouble(obj.pme.M4); //M4
        //        }
        //        if (obj.pme.M5 != "")
        //        {
        //            InputDataValue.shortmemory = System.Convert.ToDouble(obj.pme.M5);//M5
        //        }
        //        if (obj.pme.M6 != "")
        //        {
        //            InputDataValue.namingobjects = System.Convert.ToDouble(obj.pme.M6);//M6
        //        }
        //        if (obj.pme.M7 != "")
        //        {
        //            InputDataValue.languageretell = System.Convert.ToDouble(obj.pme.M7);//M7
        //        }
        //        if (obj.pme.M8 != "")
        //        {
        //            InputDataValue.readingcomprehension = System.Convert.ToDouble(obj.pme.M8);//M8
        //        }
        //        if (obj.pme.M9 != "")
        //        {
        //            InputDataValue.languageunderstanding = System.Convert.ToDouble(obj.pme.M9);//M9
        //        }
        //        if (obj.pme.M10 != "")
        //        {
        //            InputDataValue.languageexpression = System.Convert.ToDouble(obj.pme.M10);//M10
        //        }
        //        if (obj.pme.M11 != "")
        //        {
        //            InputDataValue.drawgraph = System.Convert.ToDouble(obj.pme.M11);//M11
        //        }
        //        if (obj.pma.MC1 != "")
        //        {
        //            InputDataValue.Visualspaceandexecutiveability = System.Convert.ToDouble(obj.pma.MC1);//MC1
        //        }
        //        if (obj.pma.MC2 != "")
        //        {
        //            InputDataValue.naming = System.Convert.ToDouble(obj.pma.MC2);//MC2
        //        }
        //        if (obj.pma.MC3 != "")
        //        {
        //            InputDataValue.memory = System.Convert.ToDouble(obj.pma.MC3);//MC3
        //        }
        //        if (obj.pma.MC4 != "")
        //        {
        //            InputDataValue.attention = System.Convert.ToDouble(obj.pma.MC4);//MC4
        //        }
        //        if (obj.pma.MC5 != "")
        //        {
        //            InputDataValue.language = System.Convert.ToDouble(obj.pma.MC5);//MC5
        //        }
        //        if (obj.pma.MC6 != "")
        //        {
        //            InputDataValue.animalnumber = System.Convert.ToDouble(obj.pma.MC6);//MC6
        //        }
        //        if (obj.pma.MC7 != "")
        //        {
        //            InputDataValue.abstractability = System.Convert.ToDouble(obj.pma.MC7);//MC7
        //        }
        //        if (obj.pma.MC8 != "")
        //        {
        //            InputDataValue.MoCadelayrecall = System.Convert.ToDouble(obj.pma.MC8);//MC8
        //        }
        //        if (obj.pma.MC9 != "")
        //        {
        //            InputDataValue.orientaion = System.Convert.ToDouble(obj.pma.MC9);//MC9
        //        }
        //        string[] strName = { "A1" };
        //        PatADL pal = new PatADL();
        //        ObjectMapper.CopyFrontProperties(obj.pal, pal);

        //        InputDataValue.PhysicalSelfmaintenance = System.Convert.ToDouble(pal.A1) + System.Convert.ToDouble(pal.A2) + System.Convert.ToDouble(pal.A3) + System.Convert.ToDouble(pal.A4) + System.Convert.ToDouble(pal.A5) + System.Convert.ToDouble(pal.A6) + System.Convert.ToDouble(pal.A7) + System.Convert.ToDouble(pal.A8) + System.Convert.ToDouble(pal.A9) + System.Convert.ToDouble(pal.A10);//A1+...+A10
        //        InputDataValue.grippingability = System.Convert.ToDouble(pal.A11) + System.Convert.ToDouble(pal.A12) + System.Convert.ToDouble(pal.A13) + System.Convert.ToDouble(pal.A14) + System.Convert.ToDouble(pal.A15) + System.Convert.ToDouble(pal.A16) + System.Convert.ToDouble(pal.A17) + System.Convert.ToDouble(pal.A18) + System.Convert.ToDouble(pal.A19) + System.Convert.ToDouble(pal.A20);//A11+...+A20
        //        if (obj.pot.Vocabulary1 != "")
        //        {
        //            InputDataValue.word1 = System.Convert.ToDouble(obj.pot.Vocabulary1);//Vocabulary1
        //        }
        //        if (obj.pot.Vocabulary2 != "")
        //        {
        //            InputDataValue.word2 = System.Convert.ToDouble(obj.pot.Vocabulary2);
        //        }
        //        if (obj.pot.Vocabulary3 != "")
        //        {
        //            InputDataValue.word3 = System.Convert.ToDouble(obj.pot.Vocabulary3);
        //        }
        //        InputDataValue.wordaverage = (InputDataValue.word1 + InputDataValue.word2 + InputDataValue.word3) / 3;
        //        if (obj.pot.Vocabulary4 != "")
        //        {
        //            InputDataValue.worddelayrecall = System.Convert.ToDouble(obj.pot.Vocabulary4);
        //        }
        //        if (obj.pot.VocabularyAnalyse1 != "")
        //        {
        //            InputDataValue.originalwordrecognition = System.Convert.ToDouble(obj.pot.VocabularyAnalyse1);
        //        }
        //        if (obj.pot.VocabularyAnalyse2 != "")
        //        {
        //            InputDataValue.Newwordrecognize = System.Convert.ToDouble(obj.pot.VocabularyAnalyse2);
        //        }
        //        if (obj.pot.Picture1 != "")
        //        {
        //            InputDataValue.graphcopy = System.Convert.ToDouble(obj.pot.Picture1);
        //        }
        //        if (obj.pot.Picture2 != "")
        //        {
        //            InputDataValue.graphimmediaterecall = System.Convert.ToDouble(obj.pot.Picture2);
        //        }
        //        if (obj.pot.Picture3 != "")
        //        {
        //            InputDataValue.graphdelayrecall = System.Convert.ToDouble(obj.pot.Picture3);
        //        }
        //        if (obj.pot.ConnectNumber1 != "")
        //        {
        //            InputDataValue.lineA = System.Convert.ToDouble(obj.pot.ConnectNumber1);
        //        }
        //        if (obj.pot.ConnectNumber2 != "")
        //        {
        //            InputDataValue.lineB = System.Convert.ToDouble(obj.pot.ConnectNumber2);
        //        }
        //        if (obj.pot.PatGDS != "")
        //        {
        //            InputDataValue.GDS = System.Convert.ToDouble(obj.pot.PatGDS);
        //        }
        //        if (obj.pot.PatCDR != "")
        //        {
        //            InputDataValue.CDR = System.Convert.ToDouble(obj.pot.PatCDR);
        //        }
        //        else
        //        {
        //            InputDataValue.CDR = -1;
        //        }

        //        WebReference.InferenceService b = new WebReference.InferenceService();



        //        b.DoInference(InputDataValue, ref strResult, ref dProbalily);

        //        if ("AD" == strResult)
        //            strResult = "阿尔兹海默症，具体类型待定，请结合病史";
        //        else if ("MCI" == strResult)
        //            strResult = "轻度认知功能障碍，具体类型待定，请结合病史";
        //        else if ("Normal" == strResult)
        //            strResult = "正常";
        //    }
        //    catch (Exception e)
        //    {
        //        return this.Json(new { OK = false, Message = e.Message + "推理出错" });
        //    }

        //    return this.Json(new { OK = true, Message = strResult + "              相似度:" + (dProbalily * 100).ToString("0.00") + "%" });
        //}
    }
}
