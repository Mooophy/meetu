(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileImageEditController', ProfileImageEditController);
    ProfileImageEditController.$inject = ["$scope", "$resource", "$location"];
    function ProfileImageEditController($scope, $resource, $location) {
        var vm = this;
        var User = $resource('/api/loggedUser');
        var ProFilePicture = $resource('api/ProfilePicture');

        User.query(function (user) {
            vm.loggedUser = user[0];
        }).$promise.then(function () {
            //console.log(vm.loggedUser); test
        })

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
        $scope.submit = function () {
            if ($scope.myCroppedImage == 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIwAAACMCAYAAACuwEE+AAACW0lEQVR4Xu3SsQ0AAAzCsPL/070hu5mZIu9MgVBg4euqwAEDQSoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrl/LT1AI3GBqJCAAAAAElFTkSuQmCC') {
                alert("Please select your image");
                return;
            }
            if ((/^data:image\/png;base64/.test($scope.myCroppedImage) | /^data:image\/jpeg;base64/.test($scope.myCroppedImage))==0) {
                alert("Image must be png/jpeg format");
                return;
            }
            //console.log(vm.loggedUser.userId); test
            //console.log($scope.myCroppedImage); test
            var picture = new ProFilePicture({
                userId:
                    vm.loggedUser.userId, // note: this string must be the currently logged in user's id
                data:
                    $scope.myCroppedImage,
            });
            picture.$save();
            $location.path('/Profile/' + vm.loggedUser.userId);
        }
    }
})();