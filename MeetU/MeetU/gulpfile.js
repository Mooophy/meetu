/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var minify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');



gulp.task('minifyjs', function () {
    gulp.src(['Scripts/*.js', '!Scripts/*.min.js', '!Scripts/*.min.js.map', '!Scripts/_references.js'])
        .pipe(sourcemaps.init())
        .pipe(minify())
        .pipe(sourcemaps.write())
      .pipe(gulp.dest('dist'))
});

gulp.task('minifyjs_Controllers', function () {
    gulp.src('Scripts/Controllers/**/*.js')
      .pipe(sourcemaps.init())
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js', 'min.js']
      }))
      .pipe(sourcemaps.write())
      .pipe(gulp.dest('dist/Controllers'))
});

gulp.task('minifyjs_Directives', function () {
    gulp.src('Scripts/Directives/**/*.js')
      .pipe(sourcemaps.init())
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js', 'min.js']
      }))
      .pipe(sourcemaps.write())
      .pipe(gulp.dest('dist/Directives'))
});

gulp.task('minifyjs_Helper', function () {
    gulp.src('Scripts/Helper/**/*.js')
      .pipe(sourcemaps.init())
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js', 'min.js']
      }))
      .pipe(sourcemaps.write())
      .pipe(gulp.dest('dist/Helper'))
});

gulp.task('minifyjs_Router', function () {
    gulp.src('Scripts/Router/**/*.js')
      .pipe(sourcemaps.init())
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js', 'min.js']
      }))
      .pipe(sourcemaps.write())
      .pipe(gulp.dest('dist/Router'))
});

gulp.task('minifystyles', function () {
    console.log('test');
});

gulp.task('watch', function () {
    gulp.watch('Scripts/**/**/*.js', ['minifyjs', 'minifyjs_Controllers', 'minifyjs_Directives', 'minifyjs_Helper', 'minifyjs_Router']);
})

gulp.task('default', ['minifyjs', 'minifyjs_Controllers', 'minifyjs_Directives', 'minifyjs_Helper', 'minifyjs_Router']);
