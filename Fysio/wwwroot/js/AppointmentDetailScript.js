function ReloadEvents(id) {
    console.log("in function")
    var url = '/Appointment/ReloadAppointmentDetail/' + id;
    console.log(id);
    console.log(url);
    jQuery.ajax({
        url: url,
        success: function (data) {
            console.log(data);
            document.getElementById('component').innerHTML = data;
        }
    })
}