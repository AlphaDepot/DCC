export function getSystemPreference() {
    return window.matchMedia('(prefers-color-scheme: dark)').matches;
}