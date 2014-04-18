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
            String strResult = null;
            Double dProbalily = 0.0f;
            spdata.patID = ID;
            PatBasicInfor pt = DContainer.PatBasicInforSet.Find(ID);
            VisitRecordOperation vso = new VisitRecordOperation();

            try
            {
                pt.SimpleADdata.Add(spdata);
                vso.AddNewRecord(ID);//点击启动诊断后，新建一次访问记录，并将诊断数据保存
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

              if ("Normal" == strResult)
                  strResult = "患者认知功能正常";
              else if ("Abnormal" == strResult)
                  strResult = "患者认知功能异常，具体类型待定，请结合病史";
              //else if ("Normal" == strResult)
              //    strResult = "正常";
              string Readme = Request.Form["病情自述"];
              pt.VisitRecord.Last().CDSSDiagnosis = strResult+"              相似度:" + (dProbalily * 100).ToString("0.00") + "%" ;
                DContainer.SaveChanges();
            }

            catch (Exception e)
            {
                return string.Format("false, '{0}'推理出错",e.Message);
            }      
            return string.Format("{0}    相似度：{1}", strResult, (dProbalily * 100).ToString("0.00") + "%");        
         }
      
    }
}
