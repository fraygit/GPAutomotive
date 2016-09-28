angular.module('gpApp').controller('BookController', ['$scope', '$http', function ($scope, $http) {

    $scope.Booking = { ServiceType: 'wof', DateBooked: '' };
    $scope.ShowSelectedDate = false;
    $scope.ShowBookingError = false;
    $scope.DisabledBookNow = false;
    $scope.ShowSuccess = false;
    $scope.BookingErrorMessage = '';
    $scope.SuccessMessage = '';

    $scope.TimeSlots = [];

    $scope.BookNow = function () {
        $scope.ShowBookingError = false;

        $http.put(appGlobalSettings.apiBaseUrl + '/Booking', 
            JSON.stringify($scope.Booking))
        .then(function (data) {
            $scope.ShowSuccess = true;
            $scope.SuccessMessage = "You have successfully sent the booking request. We'll contact you to confirm your booking. Thank you.";
        }, function (error) {
            $scope.ShowBookingError = true;
            $scope.BookingErrorMessage = 'Error occured.';
        });
    }

    $('#calendar').fullCalendar({
        eventSources: [{
            url: appGlobalSettings.apiBaseUrl + '/Booking?serviceType=' + $scope.Booking.ServiceType
        }],
        eventClick: function (event) {
            console.log(event);
            if (event.title == 'Available') {
                $('#selectMessage').slideUp('slow');
                $('#pnlSchedule').slideDown('slow');
                $scope.Booking.DateBooked = event.start._d;

                var dateMoment = moment(event.start._d);
                $("#selectedDate").text(dateMoment.format('Do MMM YYYY'));
                $("#pnlSelectedDate").slideDown('slow');
                
                var parameters = {
                    Date: $scope.Booking.DateBooked,
                    ServiceType: $scope.Booking.ServiceType
                };
                $http.post(appGlobalSettings.apiBaseUrl + '/TimeSlot',
                    JSON.stringify(parameters))
                .then(function (data) {
                    $scope.TimeSlots = [];
                    $.each(data.data, function (index, item) {
                        var hour = "00";
                        if ($scope.Booking.DateBooked.getDay() == 6) {
                            hour = "30";
                        }

                        if (item == 11 && $scope.Booking.DateBooked.getDay() == 6) {
                            var timeSlot = { value: item, text: item + ':00 AM' };
                            $scope.TimeSlots.push(timeSlot);
                            return true;
                        }
                        else if (item < 12) {
                            var timeSlot = { value: item, text: item + ':' + hour + ' AM' };
                            $scope.TimeSlots.push(timeSlot);
                        }
                        else if (item == 12){
                            var timeSlot = { value: item, text: item + ':' + hour + ' PM' };
                            $scope.TimeSlots.push(timeSlot);
                        }
                        else {
                            var time = item - 12;
                            var timeSlot = { value: time, text: time + ':' + hour + ' PM' };
                            $scope.TimeSlots.push(timeSlot);
                        }
                    });
                }, function (error) {
                });
            }
            else {
                $('#selectMessage').slideDown('slow');
                $('#pnlSchedule').slideUp('slow');
            }
        }
    });


}]);