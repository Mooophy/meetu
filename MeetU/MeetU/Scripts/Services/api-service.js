(function() {
    "use strict";
    angular
        .module('meetupModule')
        .factory('CommentViewService', CommentViewService);
    CommentViewService.$inject = ['$resource'];
    function CommentViewService($resource) {
        return $resource('/api/Comments/');
    }
})();