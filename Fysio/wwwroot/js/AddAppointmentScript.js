function dateChanged() {
    var date = document.getElementById("datepicker").value;
    var dates = document.getElementsByClassName("possible-date");

    for (let i = 0; i < dates.length; i++) {
        let id = dates[i].id;
        document.getElementById(id).classList.remove("visible-select");
        if (dates[i].id == date) {
            
            document.getElementById(id).classList.add("visible-select");
        }
    }

    var appointments = document.getElementsByClassName("visible-select");
    if (appointments.length == 0) {
        document.getElementById("no-dates")?.classList.remove("hidden-select");
    } else {
        document.getElementById("no-dates")?.classList.add("hidden-select");
    }

}