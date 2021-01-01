$(document).ready(function () {
    fetch("/Categories/GetCategories")
        .then(response => response.json())
        .then(data => {
            data.forEach(fillTable);
            $("#category-row").remove();

        });
});
function fillTable(item, index) {
    var categoryRow = $("#category-row").clone();
    categoryRow.removeClass("d-none");
    categoryRow.removeAttr("id");
    var count = categoryRow.find("#count");
    count.text(index + 1);
    count.removeAttr("id");

    var name = categoryRow.find("#name");
    name.text(item.Name);
    name.removeAttr("id");

    var dlt = categoryRow.find("#delete");
    dlt.attr('href', '/Categories/DeleteCategory/' + item.Id);
    dlt.removeAttr("id");

    $("#categories-table").append(categoryRow);
}