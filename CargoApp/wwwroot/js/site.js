//const { auto } = require("@popperjs/core");

$(document).ready(function () {
    let carRequestForm = $("#car-request-form");
    let cargoRequestForm = $("#cargo-request-form");
    let carBtn = $("#car-btn");
    let carBtnParent = carBtn.parent();
    let cargoBtn = $("#cargo-btn");
    let cargoBtnParent = cargoBtn.parent();

    let carSearchActive = true;

    carBtn.click(function (event) {
        if (carSearchActive) return;
        carSearchActive = true;

        carBtn.removeClass("text-black");
        carBtn.addClass("text-white");
        carBtnParent.addClass("bg-primary");

        cargoBtn.removeClass("text-white");
        cargoBtn.addClass("text-black");
        cargoBtnParent.removeClass("bg-primary");

        cargoRequestForm.hide();
        carRequestForm.show();

        searchResults.html("");

        event.preventDefault();
    });
    cargoBtn.click(function (event) {
        if (!carSearchActive) return;
        carSearchActive = false;

        cargoBtn.removeClass("text-black");
        cargoBtn.addClass("text-white");
        cargoBtnParent.addClass("bg-primary");

        carBtn.removeClass("text-white");
        carBtn.addClass("text-black");
        carBtnParent.removeClass("bg-primary");

        carRequestForm.hide();
        cargoRequestForm.show();

        searchResults.html("");

        event.preventDefault();
    });

    $(".clear-group").each(function () {
        let input = $(".clear-input", $(this));
        let btn = $(".clear-btn", $(this));
        btn.click(function () {
            input.val("");
        });
    });

    let searchResults = $("#search-results");
    if (searchResults != null) {
        $(".search-button").click(async function (e) {
            e.preventDefault();

            const url = $(this).prop("formaction");
            const form = $(this).closest("form");
            if (!form.get(0).reportValidity()) return;
            if (!form.valid()) {
                searchResults.html("");
                return;
            }
            const data = form.serialize();
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                success: function (response) {
                    searchResults.html(response);
                }
            })
        });
    }

    $(".skip-form-validation").click(function (e) {
        e.preventDefault();
        let form = $(this).closest("form").get(0).submit();
    })
});

function getSettlementAutocomplete(selector, placeHolder) {
    let autocomplete = new autoComplete({
        selector: selector,
        placeHolder: placeHolder,
        data: {
            src: async (query) => {
                try {
                    const search = await fetch(`/api/Settlements/Search?search=${query}`);
                    const data = await search.json();

                    return data;
                }
                catch (error) {
                    return error;
                }
            }
        },
        resultsList: {
            class: "settlement-list",
            element: (list, data) => {
                if (!data.results.length) {
                    const message = document.createElement("div");
                    message.setAttribute("class", "no_result");
                    message.innerHTML = `<span>Не знайдено результатів для "${data.query}"</span>`;
                    list.prepend(message);
                }
            },
            maxResults: 10,
            noResults: true,
        },
        resultItem: {
            class: "settlement-item",
            highlight: true
        },
        threshold: 3,
        debounce: 400,
        events: {
            input: {
                selection(event) {
                    const selection = event.detail.selection.value;
                    autocomplete.input.value = selection;
                },
                focus() {
                    if (autocomplete.input.value.length) autocomplete.open();
                }
            }
        }
    });
    return autocomplete;
}