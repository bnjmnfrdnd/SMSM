// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function renderResult(result) {
    if (result) {
        return '<span class="material-icons">thumb_up_alt</span>';
    }
    else
    {
        return '<span class="material-icons">thumb_down_alt</span>';
    }
} 