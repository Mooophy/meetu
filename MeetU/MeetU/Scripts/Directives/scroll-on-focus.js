(function () {
    "use strict";
    angular
        .module("meetupModule")
        .directive('scrollOnFocus', function () {
            return {
                restrict: 'A',
                link: function (scope, $element) {
                    $element.on('click', function () {
                        $("body").animate({ scrollTop: $element.offset().top }, "slow");
                    });
                }
            };
        });
})();