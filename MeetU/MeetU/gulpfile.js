/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var minify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');
var concat = require('gulp-concat');
var notify = require("gulp-notify");



gulp.task('minifyjs', function () {
    gulp.src(['Scripts/Venders/underscore.js',
               'Scripts/Venders/jquery-1.10.2.js',
               'Scripts/Venders/angular.js',
               'Scripts/Venders/*.js',
    ])
        .pipe(sourcemaps.init())
        .pipe(concat('vender.js'))
        .pipe(minify())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('Scripts/Minify_scripts'))
        .pipe(notify("Venders minify successed"));
});


gulp.task('minifyjs_all', function () {
<<<<<<< HEAD
    gulp.src(['Scripts/Controllers/Meetup/index.js',
               'Scripts/Controllers/Meetup/create.js',
               'Scripts/Controllers/Mixins/tab.js',
               'Scripts/Controllers/Profile/profile-display.js',
               'Scripts/Directives/history-back.js',
               'Scripts/Directives/subpage-nav/subpage-nav.js',
               'Scripts/Directives/loading-circle/loading-circle.js',
               'Scripts/Router/router-config.js',
               'Scripts/jquery.placepicker.js',
               'Scripts/jquery.datetimepicker.full.js',
               'Scripts/ui-bootstrap-tpls-1.2.5.js',
               'Scripts/angular-confirm.js',
               'Scripts/Directives/enter-key.js',
               'Scripts/Directives/validation.js',
               'Scripts/Directives/scroll-on-focus.js',
               'Scripts/Service/dummyDataService.js'
=======
    gulp.src([ 'Scripts/Controllers/Meetup/index.js',
               'Scripts/Controllers/**/*.js',
               'Scripts/Router/*.js',
               'Scripts/Vender_plugins/angular-moment.js',
               'Scripts/Vender_plugins/ui-bootstrap-tpls-1.2.5.js',
               'Scripts/Vender_plugins/angular-confirm.js',
               'Scripts/Vender_plugins/*.js',
               'Scripts/Directives/*.js',
>>>>>>> upstream/dev
    ])
        .pipe(sourcemaps.init())
        .pipe(concat('all.js'))
        .pipe(minify())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('Scripts/Minify_scripts'))
        .pipe(notify("All our scripts minify successed"));
});

<<<<<<< HEAD
gulp.task('default', ['minifyjs', 'minifyjs_all']);
=======
gulp.task('watch', function () {
    gulp.watch(['Scripts/**/*.js','Scripts/**/**/*.js', '!Scripts/_references.js','!Scripts/Minify_scripts/*.js'], ['minifyjs', 'minifyjs_all']);
})

gulp.task('default', ['minifyjs', 'minifyjs_all','watch']);
>>>>>>> upstream/dev
