(function() {
    "use strict";
    angular
        .module('meetupModule')
        .controller('MeetupCreateController', MeetupCreateController)
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

                    scope.$on('show-errors-check-validity', function () {
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

    MeetupCreateController.$inject = ["$log", "$q", "$resource", "$location"];
    function MeetupCreateController($log, $q, $resource, $location) {
        var vm = this;
        vm.createParams = {};
        vm.submitForm = function() {
            $resource('/api/loggedUser').query(function (userViews) {
                vm.createParams.sponsor = userViews[0].userId;
            }).$promise.then(function () {
                $log.debug(vm.createParams);
                $resource('/api/Meetups').save(vm.createParams)
                    .$promise.then(function () {
                        $log.debug(vm.createParams);
                        $location.path('/index');
                    });
            });
        }

        // TODO: should be refactored to angular style 
        $(".js-meetup-create-where").placepicker();
        $(".js-meetup-create-when").datetimepicker({minDate: '0'});
    }
})();
