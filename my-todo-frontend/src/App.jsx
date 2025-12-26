import React, { useState, useEffect } from 'react';
import { Trash2, Plus, CheckCircle, Circle, Loader2 } from 'lucide-react';
import { motion, AnimatePresence } from 'framer-motion';

const API_URL = 'https://localhost:5001/api/Todo';

// CRITICAL: Added "export default" here
export default function App() {
    const [todos, setTodos] = useState([]);
    const [taskName, setTaskName] = useState('');
    const [loading, setLoading] = useState(true);

    const fetchTodos = async () => {
        try {
            const res = await fetch(API_URL);
            const data = await res.json();
            setTodos(data);
        } catch (err) {
            console.error("Backend offline? Ensure Visual Studio is running.", err);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => { fetchTodos(); }, []);

    const handleAdd = async (e) => {
        e.preventDefault();
        if (!taskName.trim()) return;
        await fetch(`${API_URL}/Create`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ taskName })
        });
        setTaskName('');
        fetchTodos();
    };

    const handleDelete = async (id) => {
        await fetch(`${API_URL}/Delete`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id })
        });
        fetchTodos();
    };

    const handleToggle = async (id) => {
        await fetch(`${API_URL}/Toggle/${id}`, { method: 'PATCH' });
        fetchTodos();
    };

    return (
        <div className="min-h-screen bg-[#0a0a0a] bg-[url('https://images.unsplash.com/photo-1635776062127-d379bfcba9f8?q=80&w=2564')] bg-cover bg-center flex items-center justify-center p-4 font-sans">
            <motion.div
                initial={{ opacity: 0, scale: 0.9 }}
                animate={{ opacity: 1, scale: 1 }}
                className="w-full max-w-lg backdrop-blur-2xl bg-black/40 border border-white/10 rounded-[2.5rem] p-10 shadow-2xl"
            >
                <header className="mb-10 text-left">
                    <h1 className="text-5xl font-black text-white tracking-tighter mb-2 italic">
                        STRATEGY<span className="text-purple-500">.</span>
                    </h1>
                    <p className="text-white/40 font-medium tracking-wide uppercase text-xs">Tactical Task Management</p>
                </header>

                <form onSubmit={handleAdd} className="relative mb-10">
                    <input
                        value={taskName}
                        onChange={(e) => setTaskName(e.target.value)}
                        className="w-full bg-white/5 border border-white/10 rounded-2xl py-5 px-7 text-white placeholder-white/20 focus:outline-none focus:border-purple-500/50 focus:ring-4 ring-purple-500/10 transition-all duration-300"
                        placeholder="Define next objective..."
                    />
                    <button
                        type="submit"
                        className="absolute right-3 top-3 bg-white text-black h-11 w-11 rounded-xl flex items-center justify-center hover:bg-purple-500 hover:text-white transition-all duration-300 shadow-xl"
                    >
                        <Plus size={24} strokeWidth={3} />
                    </button>
                </form>

                <div className="space-y-3 max-h-[450px] overflow-y-auto pr-2">
                    {loading ? (
                        <div className="flex justify-center py-10"><Loader2 className="animate-spin text-white/20" size={32} /></div>
                    ) : (
                        <AnimatePresence mode='popLayout'>
                            {todos.map((todo) => (
                                <motion.div
                                    key={todo.id}
                                    layout
                                    initial={{ opacity: 0, y: 10 }}
                                    animate={{ opacity: 1, y: 0 }}
                                    exit={{ opacity: 0, scale: 0.95 }}
                                    className="group flex items-center justify-between bg-white/5 hover:bg-white/[0.08] border border-white/5 p-5 rounded-2xl transition-all duration-200"
                                >
                                    <div className="flex items-center gap-5">
                                        <button
                                            onClick={() => handleToggle(todo.id)}
                                            className="transition-transform active:scale-90"
                                        >
                                            {todo.isCompleted === "Yes" ? (
                                                <CheckCircle className="text-purple-400 fill-purple-400/20" size={24} />
                                            ) : (
                                                <Circle className="text-white/20 group-hover:text-white/40" size={24} />
                                            )}
                                        </button>
                                        <span className={`text-lg font-medium transition-all duration-300 ${todo.isCompleted === "Yes" ? 'text-white/20 line-through' : 'text-white/90'}`}>
                                            {todo.taskName}
                                        </span>
                                    </div>

                                    <button
                                        onClick={() => handleDelete(todo.id)}
                                        className="opacity-0 group-hover:opacity-100 text-white/20 hover:text-red-400 transition-all p-2"
                                    >
                                        <Trash2 size={20} />
                                    </button>
                                </motion.div>
                            ))}
                        </AnimatePresence>
                    )}
                </div>
            </motion.div>
        </div>
    );
}