(function () {
    "use strict";
    angular
       .module("meetupModule")
       .directive('datetimepicker', datetimepicker);
    function datetimepicker() {
        return function(scope, element, attrs) {
            element.datetimepicker({
                inline: false,
                minDate: '0',
                dateFormat: 'dd.mm.yy',
                onSelect: function(dateText) {
                    var modelPath = $(this).attr('ng-model');
                    putObject(modelPath, scope, dateText);
                    scope.$apply();
                }
            });
        }
    }

})();