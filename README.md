# Sitecore Send Proxy
.NET Core web application:
- Sitecore Send tracking implementation in .NET
- Sitecore Send SMTP email send
- Endpoint for sending Order Confirmation email using Razor View (order from [Sitecore OrderCloud](https://ordercloud.io/))
- Same, as previous, but template is managed in Sitecore Send: check [my article](https://www.brimit.com/blog/sitecore-send-5-dynamic-transactional-emails) for more details

## Usage
Run application with `dotnet run`. And open https://localhost:5001/swagger/index.html to see all the available endpoints.

### Order Confirmation email With Razor
#### `/orderconfirmation/send`
Sample body - order in json from [Sitecore OrderCloud](https://ordercloud.io/):
```json
{
   "order":{
      "ID":"Order_qe0c0c41a30eb6e6716a9_2",
      "FromUser":{
         "ID":"default-buyer-user",
         "Username":"default-buyer-user",
         "Password":null,
         "FirstName":"asdasd",
         "LastName":"asdasd",
         "Email":"sitecore.send.iz+orderconfirmation2@gmail.com",
         "Phone":null,
         "TermsAccepted":null,
         "Active":true,
         "xp":{
            "Country":"US",
            "OrderEmails":null,
            "RequestInfoEmails":null,
            "AddtlRcpts":null
         },
         "AvailableRoles":null,
         "Locale":null,
         "DateCreated":"2021-07-28T15:17:10.15+00:00",
         "PasswordLastSetDate":null
      },
      "FromCompanyID":"0001",
      "ToCompanyID":"V5VOuBjJ8Jet5Sr3",
      "FromUserID":"default-buyer-user",
      "BillingAddressID":null,
      "BillingAddress":{
         "ID":null,
         "DateCreated":null,
         "CompanyName":"asdasd",
         "FirstName":"asdasd",
         "LastName":"asdasd",
         "Street1":"asdasd",
         "Street2":"asdasd",
         "City":"NY",
         "State":"NY",
         "Zip":"10001",
         "Country":"US",
         "Phone":"1233423423",
         "AddressName":"asdasd",
         "xp":null
      },
      "ShippingAddressID":null,
      "Comments":null,
      "LineItemCount":1,
      "Status":"Open",
      "DateCreated":"2022-03-09T11:18:44.48+00:00",
      "DateSubmitted":"2022-03-09T11:19:36.853+00:00",
      "DateApproved":null,
      "DateDeclined":null,
      "DateCanceled":null,
      "DateCompleted":null,
      "LastUpdated":"2022-03-09T11:19:36.853+00:00",
      "Subtotal":10.28,
      "ShippingCost":20,
      "TaxCost":123.45,
      "PromotionDiscount":0,
      "Currency":null,
      "Total":153.73,
      "IsSubmitted":true,
      "LineItems":null,
      "xp":{
         "OrderType":"Standard",
         "QuoteOrderInfo":null
      }
   },
   "lineItems":[
      {
         "ID":"tVvi2W2xukqG8M6AFS8TNw",
         "ProductID":"KcxMdkLTBkacozLQaacSRg",
         "Quantity":1,
         "DateAdded":"2022-03-09T11:18:44.927+00:00",
         "QuantityShipped":0,
         "UnitPrice":10.28,
         "PromotionDiscount":0,
         "LineTotal":10.28,
         "LineSubtotal":10.28,
         "CostCenter":null,
         "DateNeeded":null,
         "ShippingAccount":null,
         "ShippingAddressID":null,
         "ShipFromAddressID":"004-01",
         "Product":{
            "ID":"KcxMdkLTBkacozLQaacSRg",
            "Name":"ORDINARY PORTLAND CEMENT MABANI 50kg -YAMAMA",
            "Description":"<p>Is the most common type of cement; used in general construction such as buildings, bridges, pavements, and precast units</p><p><br></p><p><strong>Manfacturer Overview</strong></p><p>Yamama cement company is one of the oldest cement companies in KSA, with daily production of more 10,000 tons of clinker</p><p><br></p><p><strong>Product Usage&nbsp;</strong></p><ul><li>General civil engineering construction work</li><li>All kinds of concrete structures</li><li>RCC works (Reinforced Cement Concrete)</li><li>Concrete Sheets and pipes&nbsp;</li><li>Blocks manufacturing&nbsp;</li><li>Non-structural works such as plastering flooring</li></ul><p><br></p>",
            "QuantityMultiplier":1,
            "ShipWeight":50,
            "ShipHeight":null,
            "ShipWidth":null,
            "ShipLength":null,
            "DefaultSupplierID":"004",
            "xp":{
               "Facets":{
                  "supplier":[
                     "Demo Supplier"
                  ]
               },
               "IntegrationData":null,
               "Status":"Draft",
               "HasVariants":false,
               "Note":"",
               "StorageConditions":"",
               "Tax":{
                  "Category":"FR000000",
                  "Code":"Headstart Tax Code",
                  "Description":"Mock Tax Code for Headstart"
               },
               "UnitOfMeasure":{
                  "Qty":50,
                  "Unit":"kg"
               },
               "ProductType":"Standard",
               "SizeTier":"A",
               "IsResale":false,
               "Accessorials":null,
               "Currency":"USD",
               "ArtworkRequired":false,
               "PromotionEligible":true,
               "FreeShipping":false,
               "FreeShippingMessage":"Free Shipping",
               "Images":[
                  {
                     "Url":"https://mydemoheadstartstorage.blob.core.windows.net/assets/4c26ba40-cd56-4e61-a9d6-a74addfeb866",
                     "ThumbnailUrl":"https://mydemoheadstartstorage.blob.core.windows.net/assets/4c26ba40-cd56-4e61-a9d6-a74addfeb866-s",
                     "Tags":null
                  }
               ],
               "Documents":null,
               "SeoName":"ordinary-portland-cement-mabani-50kg-yamama",
               "CategorySeoName":"bags"
            }
         },
         "Variant":null,
         "ShippingAddress":{
            "ID":null,
            "DateCreated":null,
            "CompanyName":"asdasd",
            "FirstName":"asdasd",
            "LastName":"asdasd",
            "Street1":"asdasd",
            "Street2":"asdasd",
            "City":"NY",
            "State":"NY",
            "Zip":"10001",
            "Country":"US",
            "Phone":"1233423423",
            "AddressName":"asdasd",
            "xp":null
         },
         "ShipFromAddress":{
            "ID":"004-01",
            "DateCreated":"2021-07-30T12:27:43.49+00:00",
            "CompanyName":"Brimit",
            "FirstName":"",
            "LastName":"",
            "Street1":"Konstanzer Strasse 86",
            "Street2":"",
            "City":"Stockstadt",
            "State":"Freistaat Bayern",
            "Zip":"63809",
            "Country":"DE",
            "Phone":"",
            "AddressName":"Supplier 1 default",
            "xp":null
         },
         "SupplierID":"004",
         "InventoryRecordID":null,
         "Specs":[
            
         ],
         "xp":{
            "StatusByQuantity":null,
            "LineTotalWithProportionalDiscounts":10.28,
            "Returns":null,
            "Cancelations":null,
            "ImageUrl":null,
            "PrintArtworkURL":null,
            "ConfigurationID":null,
            "DocumentID":null,
            "ShipMethod":null,
            "SupplierComments":null
         }
      }
   ],
   "payments":[
      {
         "ID":"6jeoynCe-ka5-hM1_wFtWA",
         "Type":"PurchaseOrder",
         "DateCreated":"2022-03-09T11:19:32.477+00:00",
         "CreditCardID":null,
         "SpendingAccountID":null,
         "Description":null,
         "Currency":null,
         "Amount":153.73,
         "Accepted":true,
         "xp":null,
         "Transactions":[
            
         ]
      }
   ]
}
```