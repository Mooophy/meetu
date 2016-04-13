(function () {
    "use strict";
    angular
        .module('meetupModule')
        .factory('EditingMeetupService', EditingMeetupService)
    function EditingMeetupService() {
         var currentlyEditing = {};
         var flag = "editing";
         return {
             getEditing: function () {
                 if (flag = "edited") {
                     return currentlyEditing;
                     flag = "editing";
                 } else {
                     console.log("getEditing Error");
                 }
             },
             setEditing: function (value) {
                 if (flag = "editing") {
                     currentlyEditing = value;
                     flag = "edited";
                 } else {
                     console.log("getEditing Error");
                 }
             },
             deleteEditing: function () {
                 currentlyEditing = {}
                 flag="editing";
             }
         }
     }   
})();