import React, { useState, useEffect } from 'react';
import './App.css';

function App() {
    const [habits, setHabits] = useState([]);
    const [loading, setLoading] = useState(true);
    const [newHabitTitle, setNewHabitTitle] = useState('');
    
    const loadHabits = () => {
        setLoading(true);
        fetch('http://localhost:5215/api/habits')
            .then(res => {
                if (!res.ok) throw new Error('Не удалось загрузить привычки');
                return res.json();
            })
            .then(data => {
                setHabits(data);
                setLoading(false);
            })
            .catch(err => {
                console.error(err);
                setLoading(false);
                alert('Ошибка при загрузке привычек');
            });
    };
    
    const handleAddHabit = (e) => {
        e.preventDefault();
        if (!newHabitTitle.trim()) return;

        fetch('http://localhost:5215/api/habits', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: newHabitTitle }),
        })
            .then(res => {
                if (!res.ok) throw new Error('Не удалось создать привычку');
                return res.json();
            })
            .then(newHabit => {
                setHabits(prev => [...prev, newHabit]);
                setNewHabitTitle('');
            })
            .catch(err => {
                console.error(err);
                alert('Не удалось добавить привычку');
            });
    };
    
    const toggleHabit = (id, currentStatus) => {
        const updatedStatus = !currentStatus;

        fetch(`http://localhost:5215/api/habits/${id}`, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ isCompleted: updatedStatus }),
        })
            .then(res => {
                if (!res.ok) throw new Error('Не удалось обновить привычку');
                return res.json(); 
            })
            .then(() => {
                setHabits(prev =>
                    prev.map(habit =>
                        habit.id === id ? { ...habit, isCompleted: updatedStatus } : habit
                    )
                );
            })
            .catch(err => {
                console.error(err);
                alert('Не удалось обновить статус');
            });
    };
    
    const deleteHabit = (id) => {
        if (!confirm('Удалить привычку?')) return;

        fetch(`http://localhost:5215/api/habits/${id}`, {
            method: 'DELETE',
        })
            .then(res => {
                if (!res.ok) throw new Error('Не удалось удалить');
                setHabits(prev => prev.filter(habit => habit.id !== id));
            })
            .catch(err => {
                console.error(err);
                alert('Ошибка при удалении');
            });
    };
    
    useEffect(() => {
        loadHabits();
    }, []);

    return (
        <div className="App">
            <h1>Трекер привычек</h1>

            {/* Форма добавления */}
            <form onSubmit={handleAddHabit} className="add-form">
                <input
                    type="text"
                    value={newHabitTitle}
                    onChange={e => setNewHabitTitle(e.target.value)}
                    placeholder="Например: Пить 2 л воды"
                    className="habit-input"
                    maxLength="100"
                />
                <button type="submit" className="btn btn-add">
                    + Добавить
                </button>
            </form>

            {/* Индикатор загрузки */}
            {loading && <p className="loading">Загрузка...</p>}

            {/* Список привычек */}
            {!loading && (
                <ul className="habits-list">
                    {habits.length === 0 ? (
                        <li className="empty-state">Нет привычек. Начните с первой!</li>
                    ) : (
                        habits.map(habit => (
                            <li key={habit.id} className="habit-item">
                                <div className="habit-info">
                  <span
                      className={`habit-title ${
                          habit.isCompleted ? 'completed' : ''
                      }`}
                      onClick={() => toggleHabit(habit.id, habit.isCompleted)}
                  >
                    {habit.title}
                  </span>
                                    <small className="habit-date">
                                        {new Date(habit.createdAt).toLocaleDateString()}
                                    </small>
                                </div>
                                <div className="habit-actions">
                                    <button
                                        onClick={() => toggleHabit(habit.id, habit.isCompleted)}
                                        className={`btn ${
                                            habit.isCompleted ? 'btn-undo' : 'btn-done'
                                        }`}
                                    >
                                        {habit.isCompleted ? '↩️ Отменить' : '✅ Выполнено'}
                                    </button>
                                    <button
                                        onClick={() => deleteHabit(habit.id)}
                                        className="btn btn-delete"
                                    >
                                        🗑️ Удалить
                                    </button>
                                </div>
                            </li>
                        ))
                    )}
                </ul>
            )}
        </div>
    );
}

export default App;