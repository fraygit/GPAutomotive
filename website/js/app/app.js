var gpApp = angular.module('gpApp', ['ui.router']);

gpApp.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/home');

    $stateProvider
        .state('home', {
            url: '',
            abstract: true,
            views: {
                'header':
                    {
                        templateUrl: 'js/app/partials/header.html'
                    }

            }
        })

        .state('home.index', {
            url: '/home',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/home.html'
                }
            }
        })

        .state('home.about', {
            url: '/about',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/about.html'
                }
            }
        })

        .state('home.servicing', {
            url: '/servicing',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/servicing.html'
                }
            }
        })

        .state('home.repairs', {
            url: '/repairs',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/repairs.html'
                }
            }
        })

        .state('home.book', {
            url: '/book',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/book.html',
                    controller: 'BookController'
                }
            }
        })

});


gpApp.controller('mainPageController', ['$scope', '$http', '$state', '$rootScope', function ($scope, $http, $state, $rootScope) {

    $scope.ShowSlider = true;

    $rootScope.$on('$stateChangeStart',
        function (event, toState, toParams, fromState, fromParams) {
            if (toState.name == 'home.index') {
                $scope.ShowSlider = true;
            }
            else {
                $scope.ShowSlider = false;
            }
        });

}]);