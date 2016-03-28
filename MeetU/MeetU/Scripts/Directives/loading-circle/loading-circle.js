(function () {
    "use strict";
    angular
        .module("meetupModule")
        .directive('loadingCircle', function(){
            return {
                restrict: 'E',
                templateUrl: '/Scripts/Directives/loading-circle/loading-circle.html',
            };
        });
})()