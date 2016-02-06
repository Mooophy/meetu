var pm = angular.module('profileModule', ['ngResource', 'ngFileUpload', 'ngImgCrop']);
pm.controller('profileController', function ($scope) {
    $scope.myImage = '';
    $scope.myCroppedImage = '';

    var handleFileSelect = function (evt) {

        $scope.isShowPic = true;

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
});