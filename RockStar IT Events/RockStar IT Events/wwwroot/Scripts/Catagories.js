var categories;
var count = 0;

function GetCategories() {

    var xhr = new XMLHttpRequest();

    xhr.open("GET", "https://eh-api.larsvanerp.com/api/categories", true);
    xhr.onload = function () {
        if (this.readyState == 4 && this.status == 200) {

            //save categorie data
            var data = JSON.parse(this.response)
            categories = data;
        }
        else if (this.readyState == 4 && this.status != 200) {
            LogError("Please try to refresh your browser");
        }
    }

    xhr.send();
}