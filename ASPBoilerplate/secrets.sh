
# Ensure the script exits if any command fails
set -e

# Initialize .NET user secrets

dotnet user-secrets set "SSLCommerze:StoreID" "tajwa624fc13a3d433"
dotnet user-secrets set "SSLCommerze:StoreSecret" "tajwa624fc13a3d433@ssl"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_51LCccnILkAUKFkLJIVOv1SdyKoWgNCYzBODNIGlGODP82zf2z34sn3DeQeuRylW5g3jte2egSaXNYoSn0QdahgHc00cPQE5ZrP"
dotnet user-secrets set "GeneralSettings:SecretKey" "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiRGV2ZWxvcGVyIiwiSXNzdWVyIjoiSXNzdWVyIiwiVXNlcm5hbWUiOiJBYnRhaGlUYWp3YXIiLCJpYXQiOjE3MzY2NDQ4NjZ9.jPtG6L_-Qm1zSZQKtPp8zEBAsCa_jbRG7j1iqO9DUF4"
dotnet user-secrets set "JwtToken:Secret" "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiRGV2ZWxvcGVyIiwiSXNzdWVyIjoiSXNzdWVyIiwiVXNlcm5hbWUiOiJBYnRhaGlUYWp3YXIiLCJpYXQiOjE3MzY2NDQ4NjZ9.jPtG6L_-Qm1zSZQKtPp8zEBAsCa_jbRG7j1iqO9DUF4"
dotnet user-secrets set "MailSettings:Password" "ce8e91d0df0043dc8e662df1a27d294b"
dotnet user-secrets set "Google:ClientSecret" "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"
dotnet user-secrets set "Google:ClientId" "733826640205-9csvn72k3r8rhb16drkrk2un96auh8hm.apps.googleusercontent.com"

echo "✅ .NET user secrets have been reinitialized!"