$(document).ready(function () {
    $('th').click(function () {

        $('th').each(function (index) {
            let text = $(this).text();
            $(this).text(text.replace('⇑', '').replace('⇓', ''));
        })

        var table = $(this).parents('table').eq(0);
        var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()));
        this.asc = !this.asc;
        const headerText = $(this).text();
        if (this.asc) {
            $(this).text(headerText + ' ⇑');
        }
        else {
            rows = rows.reverse();
            $(this).text(headerText + ' ⇓');
        }
        for (var i = 0; i < rows.length; i++) {
            table.append(rows[i]);
        }
    });

    function comparer(index) {
        return function (a, b) {
            var valA = getCellValue(a, index),
                valB = getCellValue(b, index);
            return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB);
        };
    }

    function getCellValue(row, index) {
        return $(row).children('td').eq(index).text();
    }
});