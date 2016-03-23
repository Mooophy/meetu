//  Build our app module, with a dependency on the angular modal service.
var app = angular.module('newdialog', ['angularModalService', 'ngAnimate'])
    .controller('SampleController', ['$scope', 'ModalService', function ($scope, ModalService) {

        $scope.showYesNo = function () {

            ModalService.showModal({
                templateUrl: "~/Views/Dialog/Dialog.html",
                controller: "dialogController"
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    alert(result);
                    $scope.yesNoResult = result ? "You said Yes" : "You said No";
                });
            });
        };
    }]);



var app = angular.module('newdialog');

app.controller('dialogController', ['$scope', 'close', function($scope, close) {
    alert("222");
  $scope.close = function(result) {
 	  close(result, 500); // close, but give 500ms for bootstrap to animate
  };

}]);