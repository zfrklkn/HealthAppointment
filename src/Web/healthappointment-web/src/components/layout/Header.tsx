
import { useState } from 'react';

interface HeaderProps {
    page: string;
    onNavigate: (page: string) => void;
}

const links = [
    { path: 'home', label: 'Ana Sayfa' },
    { path: 'doctors', label: 'Doktorlar' },
    { path: 'appointments', label: 'Randevularım' },
];

export default function Header({
    page,
    onNavigate,
}: HeaderProps) {
    const [menuOpen, setMenuOpen] = useState(false);

    const navigate = (path: string) => {
        onNavigate(path);
        setMenuOpen(false);
    };

    return (
        <header className="hd">
            <div className="wrap nav">
                <a
                    className="logo"
                    href="#home"
                    onClick={() => navigate('home')}
                >
                    <i>♥</i>
                    SağlıkRandevu
                </a>

                <nav
                    className={`links${menuOpen ? ' open' : ''}`}
                    aria-label="Ana menü"
                >
                    {links.map((link) => (
                        <a
                            key={link.path}
                            href={`#${link.path}`}
                            className={page === link.path ? 'on' : ''}
                            onClick={() => navigate(link.path)}
                        >
                            {link.label}
                        </a>
                    ))}

                    <a
                        href="#admin"
                        className={page === 'admin' ? 'on' : ''}
                        onClick={() => navigate('admin')}
                    >
                        Yönetim Paneli
                    </a>
                </nav>

                <button
                    className="btn o sm hide"
                    type="button"
                    onClick={() =>
                        window.alert(
                            'Kimlik doğrulama henüz geliştirilmedi.'
                        )
                    }
                >
                    Giriş Yap
                </button>

                <button
                    className="mnu"
                    type="button"
                    aria-label="Menü"
                    aria-expanded={menuOpen}
                    onClick={() => setMenuOpen(!menuOpen)}
                >
                    {menuOpen ? '✕' : '☰'}
                </button>
            </div>
        </header>
    );
}
