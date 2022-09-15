using FISPAYProject.CoreApi;
using FISPAYProject.Model;
using System.Text;

namespace pdfGenerator
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(PdfWalletModel tranData)
        {
            var sb = new StringBuilder();
            sb.Append(@"
<html>
    <head>
        <style>
header {
    padding: 10px 0;
    margin-bottom: 30px;
}

.logo {
    width: 80px;
    height: 80px;
    content: url(data:image/png;base64,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);
}

h1 {
    border-top: 1px solid green;
    border-bottom: 1px solid green;
    color: green;
    font-size: 1.4em;
    line-height: 1.4em;
    font-weight: normal;
    text-align: center;
    margin: 0 0 20px 0;
    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACsAAAAyCAAAAADg731PAAABuklEQVRIx+2Wy5LdIAxE/f//6iMJYzC+oM5iHrmVmkrm7rIYFqwkSkinu7SFXVphXRnWJeXdSniUdqekTqS6xdJlsVGWHjA0jUvZg48TPXVhUwMeWoVtpFTxpRvmLIDX1lt1oMwJt5ZTpRybpM5+SidxO8RISVKOAL+DUzp3uqRt3QfE0jLCoaWkzJSUDTywpRVw3GsLg5hSBYNL0qjhUYekCwyqNAMsNsPOJV0Y0KR5wL7vcEypAcYlrdOw7bqXpMtwo6SmQ5z9DPCpLJhjl6R1X5skzQrejKE84FyS1glHamDNoU5J2nKO6hCz4akBTVJKajCUTpsBVsfMLQywunTsVapEalWvSxlUqe6HVjXe/mYW50NK35syOJXHzn6kTiLVdk/pcYaZbV7akqT0vb8nOIan+v5xSVqt+EvvftZb+LJeyu96c45q3+zDK/19m1v/69z6+9zeeejYDv0PHjrsRv/g4YkzvuCMZ86e+C1f8Fue+X1FF6/ozZ50PJTdP3XsPTWedGybc2k5/Q0IKR+tOF7aI6V3JvClC3+O5V+x9m0vsR8v+fGSHy/5T72Eb3sJ29OuwT92jV8KLxHp9AkFrwAAAABJRU5ErkJggg==)
}

#project {
    text-align: left;
    margin-bottom:20px;
}

#span_title {
    color: darkgreen;
    text-align: right;
    width: 52px;
    margin-right: 10px;
    display: inline-block;
    font-size: 0.8em;
}
#span_txt {
    text-align: right;
    display: inline-block;
    font-size: 0.8em;
}

#span_prd {
    color: darkgreen;
    text-align: right;
    display: inline-block;
    font-size: 0.8em;
}

table {
    width: 100%;
    border-collapse: collapse;
    margin-bottom: 10px;
}

table th,
table td {
    text-align: center;
}

table th {
    padding: 5px 20px;
    color: darkgreen;
    white-space: nowrap;
    font-weight: normal;
    background-color: #f2f2f2;
    border: 0.5px solid green;
}

table td {
    padding: 10px;
    border: 0.5px solid #C1CED9;
}

footer {
    color: darkgreen;
    width: 100%;
    height: 30px;
    position: relative;
    top:5px;
}
</style>
    </head>
    <body>
        <div style='text-align: center;'><img class='logo' /></div>
        <h1>DETAILS OF STATEMENT</h1>
        <div id='project'>");
                            sb.AppendFormat(@"<div><span id='span_title>NAME </span><span id='span_txt'>{0}</span></div>
            <div><span id='span_title>MOBILE NO. </span><span id='span_txt'>{1}</span> </div>
            <div><span id='span_title>EMAIL </span><span id='span_txt'>{2}</span></div>
            <div><span id='span_title>CURRENCY </span><span id='span_txt'>INR</span></div>
            <div><span id='span_title>STATEMENT PERIOD</span><span id='span_prd'>From </span><span id='span_txt'>{3}</span><span id='span_prd'> To </span><span id='span_txt'>{4}</span></div>"
, tranData.Name, tranData.Mobile, tranData.Email, tranData.FromDateString, tranData.ToDateString);
            sb.Append(@"        
        </div>
        
        <table style='position: relative;' align='center'>
            <tr>
                <th style='width: 20%;'>Date</th>
                <th style='width: 40%;'>Remarks</th>
                <th style='width: 20%;'>Mode</th>
                <th style='width: 20%;'>Amount(Rs.)</th>
                <th style='width: 20%;'>Balance(Rs.)</th>
            </tr>");
            foreach (var td in tranData.WalletList)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                  </tr>", td.TxnDate, td.Description, td.TxnMode, td.Amount, td.RemainingBal);
            }
            sb.Append(@"
            </table>
              <footer>
                  Copyright © 2022 FIS. All Rights Reserved.
              </footer>          
        </body>
    </html>");
            return sb.ToString();
        }
    }
}
