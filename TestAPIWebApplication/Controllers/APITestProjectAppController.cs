using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TestAPIWebApplication.Controllers
{
    public class APITestProjectAppController : Controller
    {
        // GET: APITestProjectApp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveExchangeReference()
        {
            string userName = "rakibtest";
            string password = "Admin@123";

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent;
            HttpResponseMessage response = new HttpResponseMessage();


            var pairs = new List<KeyValuePair<string, string>>
                     {
                     new KeyValuePair<string, string>( "grant_type", "password" ),
                     new KeyValuePair<string, string>( "username", userName ),
                     new KeyValuePair<string, string> ( "Password", password )
                     };

            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                //response = client.PostAsync("http://114.134.95.235/apitest/Token", content).Result;
                response = client.PostAsync("http://localhost:6650/Token", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var ds = response.Content.ReadAsStringAsync().Result;
                    var jsonToken = JObject.Parse(ds);
                    var token = jsonToken["access_token"].ToString();
                    var tokenType = jsonToken["token_type"].ToString();

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, token);

                    string ExchangeCompanyCode = "6609101007";
                    string RefferenceNo = "66091111111";
                    string RemitterName = "Yousuf Rakib";
                    string RemitterPhone = "01715827404";
                    string BeneficiaryName = "Rakib Alomgir";
                    string BeneficiaryPhone = "01747854785";
                    DateTime TransDate = DateTime.Now;
                    decimal AmountLC = 10000;
                    string CurrencyCode = "USD";
                    decimal ExchangeRate = 82;
                    decimal AmountFC = 82000;
                    string BranchName = "Gulshan Branch";
                    string BranchCode = "060261726";
                    string AccountNo = "12812400000332";
                    string RemittingCountry = "BD";
                    string RemittancePurpose = "Personal";
                    string SecurityCode = "AC";
                    DateTime PaymentDate = DateTime.Now;
                    string Status = "00";

                    


                    string data = "{'ExchangeCompanyCode':'" + ExchangeCompanyCode + "'" +
                                   ",'RefferenceNo':'" + RefferenceNo + "'" +
                                   ",'RemitterName':'" + RemitterName + "'" +
                                   ",'RemitterPhone':'" + RemitterPhone + "'" +
                                   ",'BeneficiaryName':'" + BeneficiaryName + "'" +
                                   ",'BeneficiaryPhone':'" + BeneficiaryPhone + "'" +
                                   ",'TransDate':'" + TransDate + "'" +
                                   ",'AmountLC':" + AmountLC + "" +
                                   ",'CurrencyCode':'" + CurrencyCode + "'" +
                                   ",'ExchangeRate':" + ExchangeRate  + "" +
                                   ",'AmountFC':" + AmountFC + "" +
                                   ",'BranchName':'" + BranchName + "'" +
                                   ",'BranchCode':'" + BranchCode + "'" +
                                   ",'AccountNo':'" + AccountNo + "'" +
                                   ",'RemittingCountry':'" + RemittingCountry + "'" +
                                   ",'RemittancePurpose':'" + RemittancePurpose + "'" +
                                   ",'SecurityCode':'" + SecurityCode + "'" +
                                   ",'PaymentDate':'" + PaymentDate + "'" +
                                   ",'Status':'" + Status + "'}";





                    httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    
                    //response = httpClient.PostAsync("http://114.134.95.235/apitest/api/RemittInfo/Save", httpContent).Result;
                    response = httpClient.PostAsync("http://localhost:6650/api/RemittInfo/Save", httpContent).Result;
                    var resultData = response.Content.ReadAsStringAsync().Result;
                    //var json = JObject.Parse(resultData);

                    if (response.IsSuccessStatusCode)
                    {
                        
                        ViewBag.successMessage = response.StatusCode;
                    }
                    else
                    {
                        ViewBag.erroseMessage = response.StatusCode;
                    }
                }
            }

            return View();


        }
        public ActionResult SearchByExchangeReference(string id)
        {
            string userName="rakibtest";
            string password="Admin@123";
           

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent;
            HttpResponseMessage response = new HttpResponseMessage();

            if (id != null)
            {
                
                //HttpContent userNameContent = new StringContent(userName);
                //HttpContent passwordContent = new StringContent(password);
                //HttpContent grant_typeContent = new StringContent(grant_type);

                //var formDataContent = new MultipartFormDataContent();

                //formDataContent.Add(grant_typeContent, "grant_type");
                //formDataContent.Add(userNameContent, "username");
                //formDataContent.Add(passwordContent, "Password");



                var pairs = new List<KeyValuePair<string, string>>
                     {
                     new KeyValuePair<string, string>( "grant_type", "password" ),
                     new KeyValuePair<string, string>( "username", userName ),
                     new KeyValuePair<string, string> ( "Password", password )
                     };

                var content = new FormUrlEncodedContent(pairs);
                
                using (var client = new HttpClient())
                {
                    //response = client.PostAsync("http://114.134.95.235/apitest/Token", content).Result;
                    response = client.PostAsync("http://localhost:6650/Token", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var ds = response.Content.ReadAsStringAsync().Result;
                        var jsonToken = JObject.Parse(ds);
                        var token = jsonToken["access_token"].ToString();
                        var tokenType = jsonToken["token_type"].ToString();

                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, token);
                        //response = httpClient.GetAsync("http://114.134.95.235/apitest/api/RemittInfo/SingleRemittance?referenceNo=" + id).Result;
                        response = httpClient.GetAsync("http://localhost:6650/api/RemittInfo/SingleRemittance?referenceNo=" + id).Result;

                        var resultData = response.Content.ReadAsStringAsync().Result;
                        var json = JObject.Parse(resultData);

                        if (response.IsSuccessStatusCode)
                        {
                            var ExchangeCompanyCode = json["ExchangeCompanyCode"].ToString();
                            var RefferenceNo = json["RefferenceNo"].ToString();
                            var RemitterName = json["RemitterName"].ToString();
                            var RemitterPhone = json["RemitterPhone"].ToString();
                            var RemitterNationality = json["RemitterNationality"].ToString();
                            var BeneficiaryName = json["BeneficiaryName"].ToString();
                            var BeneficiaryPhone = json["BeneficiaryPhone"].ToString();
                            var TransDate = json["TransDate"].ToString();
                            var AmountLC = json["AmountLC"].ToString();
                            var CurrencyCode = json["CurrencyCode"].ToString();
                            var ExchangeRate = json["ExchangeRate"].ToString();
                            var AmountFC = json["AmountFC"].ToString();
                            var BranchName = json["BranchName"].ToString();
                            var BranchCode = json["BranchCode"].ToString();
                            var AccountNo = json["AccountNo"].ToString();
                            var RemittingCountry = json["RemittingCountry"].ToString();
                            var RemittancePurpose = json["RemittancePurpose"].ToString();
                            var SecurityCode = json["SecurityCode"].ToString();
                            var PaymentDate = json["AccountNo"].ToString();
                            var Status = json["Status"].ToString();
                            var StatusName = json["StatusName"].ToString();

                            ViewBag.successMessage = response.StatusCode;
                        }
                        else
                        {
                            ViewBag.erroseMessage = response.StatusCode;
                        }

                        
                    }
                }

                

                
                //var mess = response.Content.ReadAsStringAsync().Result;
                //var json = JObject.Parse(mess);

                ////var code2 = json.SelectTokens("Code").ToString();
                //var code = json["Code"].ToString();

                ////var message2 = json.SelectTokens("Message").ToString();
                //var message = json["Message"].ToString();

                //if (code.ToString() != "0")
                //{
                //    ViewBag.successMessage = message;
                //}
                //else
                //{
                //    ViewBag.erroseMessage = message;
                //}
            }
            return View();

            
        }

        public ActionResult InsertPinNoView()
        {

            ServiceReference1.BSELServicesClient bSELServicesClient = new ServiceReference1.BSELServicesClient();

            ServiceReference1.SessionRequest sessionRequest = new ServiceReference1.SessionRequest();



            sessionRequest.ClientId = 11;
            sessionRequest.UserId = "123";
            sessionRequest.Password = "1234";

            var SS = bSELServicesClient.GetSession(sessionRequest);

            if (SS.Code == 1001)
            {
                var ss = SS.SessionId;
            }

            //return Json(sessionRequest, JsonRequestBehavior.AllowGet);

            return View(sessionRequest);
        }

    }
    
}