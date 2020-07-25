// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

'use strict';

$(document).ready(function () {

    $('#CheckButton').on('click', textRequest);
    $('#CheckFile').on('click', fileRequest);

    const result = $('#Results');
    const maxCount = 10;

    let reponseCounter = 0;
    let textBox = $('#TextBox');
    let fileInput = $('#File');

    function textRequest(e) {
        e.preventDefault();

        let text = textBox.val();

        if (!text) {
            addElem('Warning: No text added, please add some text.')
            return;
        }

        $.ajax(
            {
                type: 'GET',
                url: 'Metrics?text=' + text,
                cache: false
            }
        ).done(showResponse, () => { textBox.val('') });
    }

    function fileRequest(e) {
        e.preventDefault();

        let file = fileInput[0].files[0]

        if (!file) {
            addElem('Warning: FIle not found. Please choose new file.')
            return;
        }

        let formdata = new FormData();
        formdata.append('file', file);

        $.ajax(
            {
                type: 'POST',
                url: 'Metrics',
                cache: false,
                data: formdata,
                processData: false,
                contentType: false
            }
        ).done(showResponse, () => { fileInput.val('') });
    }

    function showResponse(response) {
        response.forEach(item => {
            addElem(`${item.name}: ${item.value}`);
        });
    }

    function addElem(text) {
        let elem = document.createElement('div');
        elem.append(text);
        result.append(elem);
        if (reponseCounter < maxCount) {
            reponseCounter++;
        }
        else {
            result.children()[0].remove();
        }
    }

});