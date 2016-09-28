angular.module('gpaAdminApp').controller('HomeController', ['$scope', '$http', function ($scope, $http) {

    $scope.BookingRequests = [];
    $scope.FormBookingRequest = { ServiceType: 'wof' };

    $scope.ApprovedBooking = { ServiceType: 'wof', Date: new Date() };

    $scope.EditBookingRequestItem = {};

    $scope.BindList = function () {
        $http.get(appGlobalSettings.apiBaseUrl + '/BookingRequest?serviceType=' + $scope.FormBookingRequest.ServiceType,
        JSON.stringify($scope.Booking))
        .then(function (data) {
            //$scope.BookingRequests = data.data;
            var bookingRequests = [];
            $.each(data.data, function (index, item) {
                var hour = "00";
                if (new Date(item.DateBooked).getDay() == 6) {
                    hour = "30";
                }

                if (item.TimeSlot == 11 && new Date(item.DateBooked).getDay() == 6) {
                    var timeSlot = { value: item, text: item + ':00 AM' };
                    item.TimeSlot2 = item.TimeSlot + ':00 AM';
                }
                else if (item.TimeSlot < 12 && item.TimeSlot != 11) {
                    var timeSlot = { value: item, text: item + ':' + hour + ' AM' };
                    item.TimeSlot2 = item.TimeSlot + ':' + hour + ' AM';
                }
                else if (item.TimeSlot == 12) {
                    var timeSlot = { value: item, text: item + ':' + hour + ' PM' };
                    item.TimeSlot2 = item.TimeSlot + ':' + hour + ' PM';;
                }
                else {
                    var time = item.TimeSlot - 12;
                    var timeSlot = { value: time, text: time + ':' + hour + ' PM' };
                    item.TimeSlot2 = item.TimeSlot + ':' + hour + ' PM';;
                }
                bookingRequests.push(item);
            });
            $scope.BookingRequests = bookingRequests;
            sessionStorage.setItem("BookingRequest-" + $scope.FormBookingRequest.ServiceType, JSON.stringify(data.data));
        }, function (error) {
        });
    };

    $scope.BindList();

    $scope.ApproveBooking = function (bookingId, isApproved) {
        var parameters = { BookingId: bookingId, IsApproved: isApproved};
        $http.post(appGlobalSettings.apiBaseUrl + '/Booking',
            JSON.stringify(parameters))
        .then(function (data) {
            $scope.BindList();
        }, function (error) {
            $scope.BindList();
        });
    };

    $scope.BindApprovedBookingList = function () {

        var requestApprovedBooking = { Date: $scope.ApprovedBooking.Date, ServiceType: $scope.ApprovedBooking.ServiceType }

        $http.post(appGlobalSettings.apiBaseUrl + '/ApprovedBooking',
        JSON.stringify(requestApprovedBooking))
        .then(function (data) {
            $scope.ApprovedBookingList = data.data;
        }, function (error) {
        });
    };

    $scope.BindApprovedBookingList();

    $('.RequestDateApproved').datepicker().on('changeDate', function (e) {
        $scope.ApprovedBooking.Date = new Date(Date.UTC(e.date.getFullYear(), e.date.getMonth(), e.date.getDate()));
        $scope.BindApprovedBookingList();
    });


    $scope.GetEditBookingRequestTimeSlot = function () {
        var parameters = {
            Date: $scope.EditBookingRequestItem.DateBooked,
            ServiceType: $scope.EditBookingRequestItem.ServiceType
        };

        $http.post(appGlobalSettings.apiBaseUrl + '/TimeSlot',
            JSON.stringify(parameters))
        .then(function (data) {
            $scope.TimeSlots = [];
            $.each(data.data, function (index, item) {
                var hour = "00";
                if (new Date($scope.EditBookingRequestItem.DateBooked).getDay() == 6) {
                    hour = "30";
                }

                if (item == 11 && new Date($scope.EditBookingRequestItem.DateBooked).getDay() == 6) {
                    var timeSlot = { value: item, text: item + ':00 AM' };
                    $scope.TimeSlots.push(timeSlot);
                }
                else if (item < 12 && item != 11) {
                    var timeSlot = { value: item, text: item + ':' + hour + ' AM' };
                    $scope.TimeSlots.push(timeSlot);
                }
                else if (item == 12) {
                    var timeSlot = { value: item, text: item + ':' + hour + ' PM' };
                    $scope.TimeSlots.push(timeSlot);
                }
                else {
                    var time = item - 12;
                    var timeSlot = { value: time, text: time + ':' + hour + ' PM' };
                    $scope.TimeSlots.push(timeSlot);
                }
            });
            $scope.EditBookingRequestItem.TimeSlot = 2;
        }, function (error) {
        });
    }

    $scope.EditBookingRequest = function (bRequest) {

        if (sessionStorage.getItem("BookingRequest-" + $scope.FormBookingRequest.ServiceType) != null) {
            var bookingRequestItems = JSON.parse(sessionStorage.getItem("BookingRequest-" + $scope.FormBookingRequest.ServiceType));

            $.each(bookingRequestItems, function (bIndex, bItem) {
                if (bItem.Id == bRequest.Id) {
                    $scope.EditBookingRequestItem = bItem;
                    $("#pnlEditBookingRequest").modal('show');
                    $('#EditBookingRequestItemDate').datepicker({
                        format: 'yyyy-mm-dd',
                    });
                    $scope.GetEditBookingRequestTimeSlot();
                    $('#EditBookingRequestItemDate').datepicker("update", new Date(bItem.DateBooked));
                }
            });
        }
        else {
            document.location.href = "/login.html";
        }

    };

    $scope.Save = function () {

        $scope.EditBookingRequestItem.DateBooked = new Date($('#EditBookingRequestItemDate').val());

        $http.post(appGlobalSettings.apiBaseUrl + '/BookingRequest?id=' + $scope.EditBookingRequestItem.Id,
            JSON.stringify($scope.EditBookingRequestItem))
        .then(function (data) {
            $("#pnlEditBookingRequest").modal('hide');
            $scope.BindList();
        }, function (e) {
            $("#pnlEditBookingRequest").modal('hide');
        });
    }

}]);