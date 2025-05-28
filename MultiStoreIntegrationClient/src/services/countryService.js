import axios from 'axios';

const API_URL = 'https://restcountries.com/v3.1';

// Fallback country list
const fallbackCountries = [
    { name: 'Turkey', code: '+90', isoCode: 'TR', display: 'Turkey (+90)' },
    { name: 'United States', code: '+1', isoCode: 'US', display: 'United States (+1)' },
    { name: 'United Kingdom', code: '+44', isoCode: 'GB', display: 'United Kingdom (+44)' },
    { name: 'Germany', code: '+49', isoCode: 'DE', display: 'Germany (+49)' },
    { name: 'France', code: '+33', isoCode: 'FR', display: 'France (+33)' },
    { name: 'Italy', code: '+39', isoCode: 'IT', display: 'Italy (+39)' },
    { name: 'Spain', code: '+34', isoCode: 'ES', display: 'Spain (+34)' },
    { name: 'Netherlands', code: '+31', isoCode: 'NL', display: 'Netherlands (+31)' },
    { name: 'Russia', code: '+7', isoCode: 'RU', display: 'Russia (+7)' },
    { name: 'China', code: '+86', isoCode: 'CN', display: 'China (+86)' },
    { name: 'Japan', code: '+81', isoCode: 'JP', display: 'Japan (+81)' },
    { name: 'South Korea', code: '+82', isoCode: 'KR', display: 'South Korea (+82)' },
    { name: 'India', code: '+91', isoCode: 'IN', display: 'India (+91)' },
    { name: 'Brazil', code: '+55', isoCode: 'BR', display: 'Brazil (+55)' },
    { name: 'Canada', code: '+1', isoCode: 'CA', display: 'Canada (+1)' }
];

export const getCountries = async () => {
    try {
        const response = await axios.get(`${API_URL}/all?fields=name,idd,cca2`);
        if (response.data) {
            return response.data
                .filter(country => country.idd.root) // Sadece telefon kodu olan ülkeleri al
                .map(country => ({
                    name: country.name.common,
                    code: `${country.idd.root}${country.idd.suffixes ? country.idd.suffixes[0] : ''}`,
                    isoCode: country.cca2,
                    display: `${country.name.common} (${country.idd.root}${country.idd.suffixes ? country.idd.suffixes[0] : ''})`
                }))
                .sort((a, b) => a.name.localeCompare(b.name));
        }
        throw new Error('API returned no data');
    } catch (error) {
        console.error('Error fetching countries:', error);
        // API çalışmazsa en azından Türkiye'yi göster
        return [{
            name: 'Turkey',
            code: '+90',
            isoCode: 'TR',
            display: 'Turkey (+90)'
        }];
    }
};

export default {
    getCountries
}; 