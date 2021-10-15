function inputChanged() {
    var category = document.getElementById('category')?.value;
    var url = '/Patient/GetDiagnosisForCategory/?category=' + category;
    jQuery.ajax({
        url: url,
        success: function (data) {
            console.log(data);
            $('#diagnosis').html('');
            var options = '';
            options += '<option value="Select">Selecteer een optie...</option>';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].value + '">' + data[i].diagnosisDescription + '</option>';
            }
            $('#diagnosis').append(options);
        }
    })
}


function isNecessary() {
    var id = document.getElementById('type')?.value + " ";
    var url = '/Treatment/IsDescriptionNecessaryForTreatment/?id=' + id;
    jQuery.ajax({
        url: url,
        success: function (data) {
            console.log(data);
            $('#necessary').prop('checked', data);
        }
    })
}