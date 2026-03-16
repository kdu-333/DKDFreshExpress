// Direct Wholesale Supply – main.js

document.addEventListener('DOMContentLoaded', function () {
    // ── Sidebar toggle (mobile) ──────────────────────────
    const hamburger = document.getElementById('hamburger-btn');
    const sidebar   = document.getElementById('app-sidebar');
    const overlay   = document.getElementById('sidebar-overlay');

    if (hamburger && sidebar) {
        hamburger.addEventListener('click', () => {
            sidebar.classList.toggle('open');
            overlay.classList.toggle('visible');
        });
        overlay.addEventListener('click', () => {
            sidebar.classList.remove('open');
            overlay.classList.remove('visible');
        });
    }

    // ── Auto-hide alerts ──────────────────────────────────
    document.querySelectorAll('.alert-auto-hide').forEach(el => {
        setTimeout(() => {
            el.style.transition = 'opacity 0.5s';
            el.style.opacity = '0';
            setTimeout(() => el.remove(), 500);
        }, 3500);
    });

    // ── Quick Adjust buttons ──────────────────────────────
    document.querySelectorAll('.quick-adjust-btn').forEach(btn => {
        btn.addEventListener('click', async function () {
            const productId = this.dataset.product;
            const amount    = parseFloat(this.dataset.amount);
            const field     = this.dataset.field || 'both';
            const token     = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

            const row = document.querySelector(`[data-product-row="${productId}"]`);
            if (!row) return;

            try {
                const res = await fetch('/Products/QuickAdjust', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    body: `ProductID=${productId}&Amount=${amount}&Field=${field}&__RequestVerificationToken=${encodeURIComponent(token)}`
                });

                const data = await res.json();
                if (!data.success) return;

                // Update displayed prices
                const wCell = row.querySelector('.wholesale-price');
                const rCell = row.querySelector('.retail-price');

                if (wCell) {
                    wCell.textContent = '₱' + data.wholesale.toFixed(2);
                    wCell.className = 'wholesale-price price-badge ' + movementClass(data.wholesaleMovement);
                }
                if (rCell) {
                    rCell.textContent = '₱' + data.retail.toFixed(2);
                    rCell.className = 'retail-price price-badge ' + movementClass(data.retailMovement);
                }

                // Flash green
                row.style.background = '#d4edda';
                setTimeout(() => row.style.background = '', 600);

            } catch (e) { console.error(e); }
        });
    });

    function movementClass(m) {
        if (m > 0) return 'green-up';
        if (m < 0) return 'red-down';
        return '';
    }

    // ── Confirm deletes ───────────────────────────────────
    document.querySelectorAll('.confirm-delete').forEach(btn => {
        btn.addEventListener('click', function (e) {
            if (!confirm('Are you sure you want to delete this item? This cannot be undone.')) {
                e.preventDefault();
            }
        });
    });

    // ── Print button ──────────────────────────────────────
    document.getElementById('print-btn')?.addEventListener('click', () => window.print());
});
