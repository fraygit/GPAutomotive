if (sessionStorage.getItem(appGlobalSettings.sessionTokenName) == null) {
    document.location.href = "login.html";
}

var gpaAdminApp = angular.module('gpaAdminApp', ['ui.router']);

gpaAdminApp.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/home');

    $stateProvider
        .state('home', {
            url: '',
            abstract: true,
            views: {
                'header':
                    {
                        templateUrl: 'js/app/partials/header.html'
                    },
                'main-side-bar':
                    {
                        templateUrl: 'js/app/partials/mainSideBar.html'
                    },
                'breadcrumb':
                    {
                        templateUrl: 'js/app/partials/breadcrumb.html'
                    }
            }
        })

        .state('home.index', {
            url: '/home',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/home.html',
                    controller: 'HomeController'
                }
            }
        })

});