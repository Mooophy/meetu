(function () {
    "use strict";
           angular
              .module("angular-confirm")
              .directive('focused', focused);
               focused.inject = ["($timeout", "$parse"]
               function focused($timeout, $parse) {
                   return {
                       link: function ($scope, element, attributes) {
                           var model = $parse(attributes.focused);
                           $scope.$watch(model, function (value) {
                               if (value === true) {
                                   $timeout(function () {
                                       element[0].focus();
                                   });
                               }
                           });
                           // set attribute value to 'false' on blur event:
                           element.bind('blur', function () {
                               if (model && model.assign) {
                                   $scope.$apply(model.assign($scope, false));
                               }
                           });
                       }
                   };
           }
})()