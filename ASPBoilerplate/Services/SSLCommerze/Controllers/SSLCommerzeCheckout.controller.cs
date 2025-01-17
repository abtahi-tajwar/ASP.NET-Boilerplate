using System;
using System.Collections.Specialized;
using ASPBoilerplate.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace ASPBoilerplate.Services.SSLCommerze;

[ApiController]
[Route("/sslcommerz/checkout")]
public class SSLCommerzeCheckoutController : ControllerBase
{
    [HttpPost("create-checkout-session")]
    public IResult CreateCheckoutSession()
    {
        var productName = "HP Pavilion Series Laptop";
        var price = 85000;

        var baseUrl = Request.Scheme + "://" + Request.Host;

        // CREATING LIST OF POST DATA
        NameValueCollection PostData = new NameValueCollection();

        PostData.Add("total_amount", $"{price}");
        PostData.Add("tran_id", "TESTASPNET1234");
        PostData.Add("success_url", baseUrl + "/Cart/CheckoutConfirmation");
        PostData.Add("fail_url", baseUrl + "/Cart/CheckoutFail");
        PostData.Add("cancel_url", baseUrl + "/Cart/CheckoutCancel");

        PostData.Add("version", "3.00");
        PostData.Add("cus_name", "ABC XY");
        PostData.Add("cus_email", "abc.xyz@mail.co");
        PostData.Add("cus_add1", "Address Line On");
        PostData.Add("cus_add2", "Address Line Tw");
        PostData.Add("cus_city", "City Nam");
        PostData.Add("cus_state", "State Nam");
        PostData.Add("cus_postcode", "Post Cod");
        PostData.Add("cus_country", "Countr");
        PostData.Add("cus_phone", "0111111111");
        PostData.Add("cus_fax", "0171111111");
        PostData.Add("ship_name", "ABC XY");
        PostData.Add("ship_add1", "Address Line On");
        PostData.Add("ship_add2", "Address Line Tw");
        PostData.Add("ship_city", "City Nam");
        PostData.Add("ship_state", "State Nam");
        PostData.Add("ship_postcode", "Post Cod");
        PostData.Add("ship_country", "Countr");
        PostData.Add("value_a", "ref00");
        PostData.Add("value_b", "ref00");
        PostData.Add("value_c", "ref00");
        PostData.Add("value_d", "ref00");
        PostData.Add("shipping_method", "NO");
        PostData.Add("num_of_item", "1");
        PostData.Add("product_name", $"{productName}");
        PostData.Add("product_profile", "general");
        PostData.Add("product_category", "Demo");

        //we can get from email notificaton
        var storeId = SSLCommerzeSettings.StoreID;
        var storePassword = SSLCommerzeSettings.StoreSecret;
        var isSandboxMood = true;

        SSLCommerzeService sslcz = new SSLCommerzeService(storeId, storePassword, isSandboxMood);

        string response = sslcz.InitiateTransaction(PostData);

        return CustomResponse.Ok(response);
    }
}
