(function () {
    "use strict";
    angular
       .module("meetupModule")
       .directive('googleplace', googleplace);
    function googleplace() {
        return {
            link: function (scope, element, attrs) {
                var options = {
                    types: [],
                    componentRestrictions: { country: 'in' }
                };
                scope.gPlace = new google.maps.places.Autocomplete(element[0], options);
                element.blur(function (e) {
                    window.setTimeout(function () {
                        angular.element(element).trigger('input');
                    }, 0);
                });
            }

        };
    }
})();