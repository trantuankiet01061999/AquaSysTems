// wwwroot/js/reportCharts.js
// Requires Chart.js loaded via CDN in index.html:
// <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/4.4.1/chart.umd.js"></script>

const _charts = {};

function destroyChart(id) {
    if (_charts[id]) {
        _charts[id].destroy();
        delete _charts[id];
    }
}

window.destroyAllCharts = function () {
    Object.keys(_charts).forEach(destroyChart);
};

const COLORS = ['#378ADD', '#1D9E75', '#BA7517', '#D85A30', '#7F77DD', '#D4537E', '#639922'];
const STATUS_COLORS = ['#639922', '#EF9F27', '#E24B4A'];

function kFmt(v) {
    return v >= 1000 ? (v / 1000).toFixed(1) + 'k' : v;
}

window.renderPieChart = function (canvasId, labels, data) {
    destroyChart(canvasId);
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;
    _charts[canvasId] = new Chart(ctx, {
        type: 'pie',
        data: {
            labels,
            datasets: [{ data, backgroundColor: COLORS, borderWidth: 2, borderColor: '#fff' }]
        },
        options: {
            responsive: true,
            plugins: { legend: { position: 'bottom', labels: { font: { size: 11 } } } }
        }
    });
};

window.renderCompareBarChart = function (canvasId, labels, registered, confirmed) {
    destroyChart(canvasId);
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;
    _charts[canvasId] = new Chart(ctx, {
        type: 'bar',
        data: {
            labels,
            datasets: [
                { label: 'Đăng ký (kg)', data: registered, backgroundColor: '#378ADD', borderRadius: 3 },
                { label: 'Nhà rác nhận (kg)', data: confirmed, backgroundColor: '#1D9E75', borderRadius: 3 }
            ]
        },
        options: {
            responsive: true,
            plugins: { legend: { position: 'bottom', labels: { font: { size: 11 } } } },
            scales: {
                x: { ticks: { font: { size: 11 } } },
                y: { ticks: { font: { size: 11 }, callback: kFmt } }
            }
        }
    });
};

window.renderHBarChart = function (canvasId, labels, data) {
    destroyChart(canvasId);
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;
    const height = Math.max(200, labels.length * 38 + 60);
    ctx.parentElement.style.height = height + 'px';
    _charts[canvasId] = new Chart(ctx, {
        type: 'bar',
        data: {
            labels,
            datasets: [{ label: 'Khối lượng (kg)', data, backgroundColor: COLORS, borderRadius: 3 }]
        },
        options: {
            indexAxis: 'y',
            responsive: true,
            maintainAspectRatio: false,
            plugins: { legend: { display: false } },
            scales: {
                x: { ticks: { font: { size: 10 }, callback: kFmt } },
                y: { ticks: { font: { size: 11 } } }
            }
        }
    });
};

window.renderLineChart = function (canvasId, labels, data) {
    destroyChart(canvasId);
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;
    _charts[canvasId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels,
            datasets: [{
                label: 'Số đơn',
                data,
                borderColor: '#7F77DD',
                backgroundColor: 'rgba(127,119,221,0.12)',
                fill: true,
                tension: 0.4,
                pointRadius: 4
            }]
        },
        options: {
            responsive: true,
            plugins: { legend: { display: false } },
            scales: {
                x: { ticks: { font: { size: 11 } } },
                y: { ticks: { font: { size: 11 } } }
            }
        }
    });
};

window.renderDoughnutChart = function (canvasId, labels, data) {
    destroyChart(canvasId);
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;
    _charts[canvasId] = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels,
            datasets: [{ data, backgroundColor: STATUS_COLORS, borderWidth: 2, borderColor: '#fff' }]
        },
        options: {
            responsive: true,
            cutout: '65%',
            plugins: {
                legend: { position: 'bottom', labels: { font: { size: 11 } } }
            }
        }
    });
};

// Utility: trigger file download from byte array
window.downloadFile = function (fileName, mimeType, bytes) {
    const blob = new Blob([new Uint8Array(bytes)], { type: mimeType });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    a.click();
    URL.revokeObjectURL(url);
};
