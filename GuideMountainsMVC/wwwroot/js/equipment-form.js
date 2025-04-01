$(document).ready(function () {
    $('#CountryId').change(function () {
        var countryId = $(this).val();

        if (countryId) {
            $.get('/EquipmentRental/GetMountainPlacesByCountryId', { countryId: countryId }, function (data) {
                $('#MountainPlaceId').empty();
                $('#MountainPlaceId').append('<option value="">Select a mountain place</option>');

                $.each(data, function (index, place) {
                    $('#MountainPlaceId').append('<option value="' + place.value + '">' + place.text + '</option>');
                });
            });
        } else {
            $('#MountainPlaceId').empty();
            $('#MountainPlaceId').append('<option value="">Select a mountain place</option>');
        }
    });
});
