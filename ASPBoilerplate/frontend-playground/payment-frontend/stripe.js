
var publicKey = "pk_test_51LCccnILkAUKFkLJNsJr4r4yrHvSumlZhvl6hH7OZmKBNgDb1SWu2rTmcINrzHpYs07IZflfzR65awcNQod9cNzl00rBOiejlc"
var stripe = Stripe(publicKey);
document
  .getElementById("checkout-button")
  .addEventListener("click", function () {
    fetch(`${apiUrl}/checkout/create-checkout-session`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        productName: document.getElementById("productName").value,
        productDescription: document.getElementById("productDescription").value,
        amount: document.getElementById("amount").value,
        currency: document.getElementById("currency").value,
      }),
    })
      .then(function (response) {
        return response.json();
      })
      .then(function (session) {
        return stripe.redirectToCheckout({ sessionId: session.sessionId });
      })
      .then(function (result) {
        if (result.error) {
          alert(result.error.message);
        }
      })
      .catch(function (error) {
        console.error("Error:", error);
      });
  });
