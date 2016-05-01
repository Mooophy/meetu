(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileImageEditController', ProfileImageEditController);
    ProfileImageEditController.$inject = ["$scope"];
    function ProfileImageEditController($scope) {
        $scope.myImage = '';
        $scope.myCroppedImage = '';

        var handleFileSelect = function (evt) {
            var file = evt.currentTarget.files[0];
            var reader = new FileReader();
            reader.onload = function (evt) {
                $scope.$apply(function ($scope) {
                    $scope.myImage = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        };
        angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
    }
})();