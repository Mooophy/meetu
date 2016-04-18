(function () {
    "use strict";
    angular
       .module("meetupModule")
       .directive('placepicker', placepicker);
    function placepicker() {
        return function (scope, element, attrs) {
            element.placepicker({
                onSelect: function (placeText) {
                    var modelPath = $(this).attr('ng-model');
                    putObject(modelPath, scope, placeText);
                    scope.$apply();
                }
            });
        }
    }

})();