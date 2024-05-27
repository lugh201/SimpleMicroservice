async function fetchBooks() {
    const response = await fetch('http://localhost:5099/books');
    if (!response.ok) {
        console.error('Failed to fetch books:', response.statusText);
        return;
    }
    const books = await response.json();
    const tableBody = document.getElementById('book-table').querySelector('tbody');
    books.forEach(book => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${book.title}</td>
            <td>${book.author}</td>
            <td>${book.description}</td>
            <td>${book.publishedDate}</td>
        `;
        tableBody.appendChild(row);
    });
}

document.addEventListener('DOMContentLoaded', fetchBooks);
