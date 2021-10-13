function searchFunction() {
    var search = document.getElementById("searchfield")?.value;

    var items = document.getElementsByClassName("patient-item");
    console.log(items);
    console.log(search);

    for (var i = 0; i < items.length; i++) {
        let item = items[i];
        console.log(item);
        console.log(item.id);
        if (!item.id.toLowerCase().includes(search.toLowerCase())) {
            item.classList.add("hidden");
            console.log("onzichtbaar")
        } else {
            item.classList.remove("hidden");
        }
    }
}   