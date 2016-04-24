(function () {
    "use strict";
           angular
              .module("meetupModule")
              .directive('myValidation', function () {
            return {
                restrict: 'A',
                require: '^form',
                link: function (scope, el, attrs, formCtrl) {

                    var inputEl = el[0].querySelector("[name]");

                    var inputNgEl = angular.element(inputEl);

                    var inputName = inputNgEl.attr('name');

                    var blurred = false;
                    inputNgEl.bind('blur', function () {
                        blurred = true;
                        el.toggleClass('has-error', formCtrl[inputName].$invalid);
                    });

                    scope.$watch(function () {
                        return formCtrl[inputName].$invalid
                    }, function (invalid) {

                        if (!blurred && invalid) { return }
                        el.toggleClass('has-error', invalid);
                    });

                    scope.$on('show-errors-event', function () {
                        el.toggleClass('has-error', formCtrl[inputName].$invalid);
                    });

                    scope.$on('show-errors-reset', function () {
                        $timeout(function () {
                            el.removeClass('has-error');
                        }, 0, false);
                    });
                }
            }
        });
})();