﻿var loadcount = 1;

$(document).ready(function () {

    $("#searchButton").on("click", function (e) {

        $("#idSearch").trigger('submit');
    });

    
});

function fetchTwenty(accountid) {  

    $.ajax({
        url: "/Account/LoadTwentyMoreTransactions?accountid=" + accountid + "&nrToSkip=" + loadcount,
        success: function (result) {
            console.dir(result);

            if (result.anyMore == false) {
                $("#loadTwentyButton").hide();
            }
            
            for (let post of result.transactions) {

                let tablerow = document.createElement("tr");
                let tablehead = document.createElement("th");
                tablehead.setAttribute("scope", "row");
                let headtext = document.createTextNode(post.transactionId);
                tablehead.appendChild(headtext);
                tablerow.appendChild(tablehead);

                
                for (let x in post)
                {
                    if (x == "transactionId") {
                        continue;
                    }
                    let tabledata = document.createElement("td");
                    let text = document.createTextNode(post[x]);
                    tabledata.appendChild(text);
                    tablerow.appendChild(tabledata);
                }
                $("tbody").append(tablerow);
            }           
            loadcount += 1;
        }
    })

}

function SearchCustomers() {

    var uri = updateQueryStringParameter(window.location.href, 'searchName', $('#SearchName').val());
    window.location.href = updateQueryStringParameter(uri, 'searchCity', $('#SearchCity').val());
}

