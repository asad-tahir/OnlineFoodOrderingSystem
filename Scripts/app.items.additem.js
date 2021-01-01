$(document).ready(function () {
    fetch("/Categories/GetCategories")
        .then(response => response.json())
        .then(data => {
            data.forEach(fillTable);
            $("#options-loading").remove();//CategoryIds
        });
});
function fillTable(item) {
    var categoryOption = $("#options-loading").clone();
    categoryOption.removeAttr("id");
    categoryOption.val(item.Id);
    categoryOption.text(item.Name);

    $("#CategoryIds").append(categoryOption);
}