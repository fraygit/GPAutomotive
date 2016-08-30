var gpaAdminLoginApp = angular.module('gpaAdminLoginApp', []);

angular.module('gpaAdminLoginApp').controller('LoginController', ['$scope', '$http', function ($scope, $http) {
    
    $scope.LoginError = { Message: 'Invalid username or password', ShowError: false };

    $scope.LoginForm = { Email: '', Password: '' };

    $scope.login = function () {
        if (!commonHelper.IsStringNullOrEmpty($scope.LoginForm.Email) && !commonHelper.IsStringNullOrEmpty($scope.LoginForm.Password)) {
            $http.post(appGlobalSettings.apiBaseUrl + '/User',
                JSON.stringify($scope.LoginForm))
                .then(function (data) {
                    sessionStorage.setItem(appGlobalSettings.sessionTokenName, data.data);
                    document.location.href = "/";
                }, function (error) {
                    $scope.LoginError.Message = "Invalid username or password.";
                    $scope.LoginError.ShowError = true;
                });
            return;
        }
        else {
            $scope.LoginError.ShowError = true;
            $scope.LoginError.Message = "Please fill in all required fields.";
        }
    };

}]);