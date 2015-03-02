$(function () {
    $("#addField").click(function () { addField(); });
    function addField() {
        var $group = $("<div class='group' />");
        var $title = $("<h3 />");
        var $titleSpan = $("<span />");
        var $inputs = $("<div />");

        var $formGroupDesc = $("<div class='form-group' />");
        var $labelDesc = $("<label />");
        var $inputDesc = $("<input class='form-control' />");

        var $formGroupTipo = $("<div class='form-group' />");
        var $labelTipo = $("<label />");
        var $selectTipo = $("<select class='form-control' />");
        var $optionText = $("<option>text</option>");
        var $optionTextarea = $("<option>textarea</option>");
        var $optionCheckbox = $("<option>checkbox</option>");
        var $optionSelect = $("<option>select</option>");
        var $optionRadio = $("<option>radio</option>");

        var $formGroupLista = $("<div class='form-group' style='display: none' />");
        var $labelLista = $("<label />");
        var $inputLista = $("<input class='form-control' />");

        $group.append($title, $inputs);
        $title.append($titleSpan);
        $inputs.append($formGroupDesc, $formGroupTipo, $formGroupLista);
        $formGroupDesc.append($labelDesc, $inputDesc);
        $formGroupTipo.append($labelTipo, $selectTipo);
        $formGroupLista.append($labelLista, $inputLista);
        $selectTipo.append($optionText);
        $selectTipo.append($optionTextarea);
        $selectTipo.append($optionCheckbox);
        $selectTipo.append($optionSelect);
        $selectTipo.append($optionRadio);

        $titleSpan.text("Campo");
        $labelDesc.append("Descrição");
        $labelTipo.append("Tipo");
        $labelLista.append("Lista");

        $inputDesc.attr("placeholder", "Campo");
        $inputDesc.change(function () { var v = $(this).val(); $titleSpan.text(v); });

        $inputLista.attr("placeholder", "Separados por vírgula");

        $selectTipo.change(function () {
            switch ($(this).val()) {
                case "text": $inputLista.val(""); $formGroupLista.hide();
                    break;
                case "textarea": $inputLista.val(""); $formGroupLista.hide();
                    break;
                case "checkbox": $inputLista.val(""); $formGroupLista.show();
                    break;
                case "select": $inputLista.val(""); $formGroupLista.show();
                    break;
                case "radio": $inputLista.val(""); $formGroupLista.show();
                    break;
                default:
                    break;
            }
        });

        $("#accordion").append($group);
        $("#accordion").accordion("refresh");
    }

    $("#accordion")
        .accordion({
            header: "> div > h3"
        })
        .sortable({
            axis: "y",
            handle: "h3",
            stop: function (event, ui) {
                // IE doesn't register the blur when sorting
                // so trigger focusout handlers to remove .ui-state-focus
                ui.item.children("h3").triggerHandler("focusout");

                // Refresh accordion to handle new order
                $(this).accordion("refresh");
            }
        });

});