// site.js ou outro arquivo JS

// Inicialização para usuários ES
import {Input, Dropdown, Collapse, initMDB} from './mdb.es.min.js'

// Inicializa o MDB
initMDB({Input, Dropdown, Collapse});

document.addEventListener("DOMContentLoaded", function () {
    const timeout = 3000; // Tempo em milissegundos (5 segundos)

    setTimeout(function () {
        const alerts = document.querySelectorAll('.alert');
        alerts.forEach(function (alert) {
            alert.classList.remove('show');
            setTimeout(function () {
                alert.remove();
            }, 150); // Duração da animação de fade (150ms)
        });
    }, timeout);
});

