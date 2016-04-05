(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        define(['angular'], factory);
    } else if (typeof module !== 'undefined' && typeof module.exports === 'object') {
        module.exports = factory(require('angular'));
    } else {
        return factory(root.angular);
    }
}(this, function (angular) {
    angular.module('angular-confirm', ['ui.bootstrap.modal'])
        .controller('ConfirmModalController', ConfirmModalController)
        .value('$confirmModalDefaults', {
            template: '<div class="modal-header" ng-enter="ok()"><h3 class="modal-title">{{data.title}}</h3></div>' +
            '<div class="modal-body">{{data.text}}</div>' +
            '<div class="modal-footer">' +
            '<button class="btn btn-primary" ng-click="ok()" focused="true">{{data.ok}}</button>' +
            '<button class="btn btn-default" ng-click="cancel()">{{data.cancel}}</button>' +
            '</div>',
            controller: 'ConfirmModalController',
            defaultLabels: {
                title: 'Confirm',
                ok: 'OK',
                cancel: 'Cancel'
            }
        })
        .factory('$confirm', $confirm)
        .directive('confirm', confirm)
    ConfirmModalController.$inject = ["$scope", "$uibModalInstance", "data"];
    function ConfirmModalController($scope, $uibModalInstance, data) {
        $scope.data = angular.copy(data);

        $scope.ok = function (closeMessage) {
            $uibModalInstance.close(closeMessage);
        };

        $scope.cancel = function (dismissMessage) {
            if (angular.isUndefined(dismissMessage)) {
                dismissMessage = 'cancel';
            }
            $uibModalInstance.dismiss(dismissMessage);
        };

    }
    $confirm.$inject = ["$uibModal", "$confirmModalDefaults"];
    function $confirm($uibModal, $confirmModalDefaults) {
        return function (data, settings) {
            var defaults = angular.copy($confirmModalDefaults);
            settings = angular.extend(defaults, (settings || {}));

            data = angular.extend({}, settings.defaultLabels, data || {});

            if ('templateUrl' in settings && 'template' in settings) {
                delete settings.template;
            }

            settings.resolve = {
                data: function () {
                    return data;
                }
            };

            return $uibModal.open(settings).result;
        };
    }
    confirm.$inject = ["$confirm"];
    function confirm($confirm) {
        return {
            priority: 1,
            restrict: 'A',
            scope: {
                confirmIf: "=",
                ngClick: '&',
                confirm: '@',
                confirmSettings: "=",
                confirmTitle: '@',
                confirmOk: '@',
                confirmCancel: '@'
            },
            link: function (scope, element, attrs) {

                element.unbind("click").bind("click", function ($event) {

                    $event.preventDefault();

                    if (angular.isUndefined(scope.confirmIf) || scope.confirmIf) {

                        var data = { text: scope.confirm };
                        if (scope.confirmTitle) {
                            data.title = scope.confirmTitle;
                        }
                        if (scope.confirmOk) {
                            data.ok = scope.confirmOk;
                        }
                        if (scope.confirmCancel) {
                            data.cancel = scope.confirmCancel;
                        }
                        $confirm(data, scope.confirmSettings || {}).then(scope.ngClick);
                    } else {

                        scope.$apply(scope.ngClick);
                    }
                });

            }
        }
    }

}));
