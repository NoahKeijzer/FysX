function dateChanged(){
    document.getElementById("select")?.classList.remove("hidden-select");
    let date = document.getElementById("datepicker") as HTMLInputElement;
    document.getElementById("select").textContent = date.value;
}