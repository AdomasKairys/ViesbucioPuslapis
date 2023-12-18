using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json.Nodes;
using ViesbucioPuslapis.Data;
using Microsoft.Extensions.Logging;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    [IgnoreAntiforgeryToken]
    public class CheckoutPaymentModel : PageModel
    {
        public string PaypalClientId { get; set; } = "";
        public string PaypalSecret { get; set; } = "";
        public string PaypalUrl { get; set; } = "";

        public string RoomNr { get; set; } = "";
        public string Days { get; set; } = "";
        public string Cost { get; set; } = "";
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }


        public double reservationCost;
        public Room room { get; set; }
        public List<Reservation> reservations { get; set; }


        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;

        public CheckoutPaymentModel(ILogger<ErrorModel> logger, HotelDbContext db, IConfiguration configuration) 
        {
            PaypalClientId = configuration["PaypalSettings:ClientId"]!;
            PaypalSecret = configuration["PaypalSettings:Secret"]!;
            PaypalUrl = configuration["PaypalSettings:Url"]!;
            _logger = logger;
            _db = db;
        }



        public void OnGet()
        {           
            RoomNr = TempData["RoomNr"]?.ToString() ?? "";
            Days = TempData["Days"]?.ToString() ?? "";
            Cost = TempData["Cost"]?.ToString() ?? "";
            string start = TempData["start"]?.ToString() ?? "";
            string end = TempData["end"]?.ToString() ?? "";

            TempData.Keep();

            if (RoomNr == "" ||  Days == "" || Cost == "")
            {
                Response.Redirect("/");
                return;
            }
        }

        public void PostReservationToDb(int status) {
            string start = TempData["start"]?.ToString() ?? "";
            string end = TempData["end"]?.ToString() ?? "";
            RoomNr = TempData["RoomNr"]?.ToString() ?? "";

            Startdate = DateTime.Parse(start);
            Enddate = DateTime.Parse(end);

            TimeSpan timeSpent = Enddate - Startdate;
            var days = timeSpent.TotalDays;

            reservations = _db.kambario_rezervacija.ToList();
            room = _db.kambarys.First(r => r.kambario_numeris == RoomNr);

            reservationCost = days * room.nakties_kaina;

            //int userid = 2; //tempID
            var user = HttpContext.Session.GetComplexData<User>("user");
            int userid = user.id_Naudotojas;

            //reservationid calculation
            var lastReservation = reservations.Last();
            int resId = lastReservation.id_Kambario_rezervacija + 1;
            TempData["resId"] = resId;


            _db.Add(new Reservation
            {
                id_Kambario_rezervacija = resId,
                pradþia = Startdate,
                pabaiga = Enddate,
                kaina = Decimal.Parse(reservationCost.ToString()),
                mokejimo_busena = status,
                fk_Klientas_id_Naudotojas = userid,
                fk_Kambarys_kambario_numeris = room.kambario_numeris
            });
            _db.SaveChanges();
        }

        public void UpdateReservation(int status)
        {
            int userid = 2; //temp
            int resId = (int)TempData["resId"];

            reservations = _db.kambario_rezervacija.Where(res => res.fk_Klientas_id_Naudotojas == userid).ToList();

            Reservation reservation = reservations.First(res => res.id_Kambario_rezervacija == resId);

            if (reservation != null)
            {
                reservation.mokejimo_busena = status;
                _db.SaveChanges();
            }

        }

        public JsonResult OnPostCreateOrder()
        {
            RoomNr = TempData["RoomNr"]?.ToString() ?? "";
            Days = TempData["Days"]?.ToString() ?? "";
            Cost = TempData["Cost"]?.ToString() ?? "";

            TempData.Keep();

            if (RoomNr == "" || Days == "" || Cost == "")
            {
                return new JsonResult("");
            }

            // create the request body
            JsonObject createOrderRequest = new JsonObject();
            createOrderRequest.Add("intent", "CAPTURE");

            JsonObject amount = new JsonObject();
            amount.Add("currency_code", "USD");
            amount.Add("value", Cost);

            JsonObject purchaseUnit1 = new JsonObject();
            purchaseUnit1.Add("amount", amount);

            JsonArray purchaseUnits = new JsonArray();
            purchaseUnits.Add(purchaseUnit1);

            createOrderRequest.Add("purchase_units", purchaseUnits);


            // get access token
            string accessToken = GetPaypalAccessToken();


            // send request
            string url = PaypalUrl + "/v2/checkout/orders";

            string orderId = "";
            using (var client  = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(createOrderRequest.ToString(), null, "application/json");

                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResponse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strResponse);
                    if(jsonResponse != null)
                    {
                        orderId = jsonResponse["id"]?.ToString() ?? "";
                        // save order in the database
                        PostReservationToDb(2);
                    }
                }
            }

            var response = new
            {
                Id = orderId
            };
            return new JsonResult(response);
        }

        public JsonResult OnPostCompleteOrder([FromBody] JsonObject data)
        {
            if (data == null || data["orderID"] == null) return new JsonResult("");

            var orderID = data["orderID"]!.ToString();

            // get access token
            string accessToken = GetPaypalAccessToken();

            string url = PaypalUrl + "/v2/checkout/orders/" + orderID + "/capture";

            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("", null, "application/json");

                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResponse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
                        if(paypalOrderStatus == "COMPLETED")
                        {
                            // clear TempData
                            //TempData.Clear();

                            // update payment status in the database
                            UpdateReservation(1);

                            return new JsonResult("success");
                            
                        }
                      
                    }
                }
            }
            return new JsonResult("");
        }

        public JsonResult OnPostCancelOrder([FromBody] JsonObject data)
        {
            if (data == null || data["orderID"] == null) return new JsonResult("");

            var orderID = data["orderID"]!.ToString();

            return new JsonResult("");
        }

        private string GetPaypalAccessToken()
        {
            string accessToken = "";

            string url = PaypalUrl + "/v1/oauth2/token";

            using (var client = new HttpClient())
            {
                string credentials64 =
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(PaypalClientId + ":" + PaypalSecret));

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials64);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("grant_type=client_credentials", null
                    , "application/x-www-form-urlencoded");

                var responceTask = client.SendAsync(requestMessage);
                responceTask.Wait();

                var result = responceTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResponse = readTask.Result;

                    var jsonResponse = JsonNode.Parse(strResponse);
                    if(jsonResponse != null)
                    {
                        accessToken = jsonResponse["access_token"]?.ToString() ?? "";
                    }
                }
            }

            return accessToken;
        }
    }
}
